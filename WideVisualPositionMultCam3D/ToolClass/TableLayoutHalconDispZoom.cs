using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WideVisualPositionMultCam3D.ToolClass
{
    /// <summary>
    /// Halcon窗口布局控制器
    /// 用于控制TableLayoutPanel中的HWindowControl最大化显示
    /// </summary>
    public class TableLayoutHalconDispZoom
    {


        private TableLayoutPanel tableLayoutPanel;
        private List<HWindowControl> hWindowControls = new List<HWindowControl>();
        private bool isMaximized = false;
        private HWindowControl currentMaxWindow = null;
        private Dictionary<int, int> originalRowHeights = new Dictionary<int, int>();
        private Dictionary<int, int> originalColumnWidths = new Dictionary<int, int>();

        // 用于双击检测
        private DateTime lastClickTime = DateTime.MinValue;
        private HWindowControl lastClickedWindow = null;
        private const int DoubleClickInterval = 300; // 毫秒

        // 事件定义
        public event EventHandler<HWindowControl> OnWindowMaximized;
        public event EventHandler OnLayoutRestored;
        public event EventHandler<HWindowControl> OnWindowDoubleClicked;
        public event EventHandler<HWindowControl> OnWindowClicked;

        /// <summary>
        /// 是否启用双击最大化
        /// </summary>
        public bool EnableDoubleClickMaximize { get; set; } = true;

        /// <summary>
        /// 是否启用单击已放大的窗口恢复原状
        /// </summary>
        public bool EnableClickToRestore { get; set; } = true;

        /// <summary>
        /// 是否显示手型光标
        /// </summary>
        public bool ShowHandCursor { get; set; } = true;

        /// <summary>
        /// 单击时是否立即最大化
        /// </summary>
        public bool MaximizeOnSingleClick { get; set; } = true;

        /// <summary>
        /// 双击时是否最大化
        /// </summary>
        public bool MaximizeOnDoubleClick { get; set; } = true;

        /// <summary>
        /// 当前是否处于最大化状态
        /// </summary>
        public bool IsMaximized => isMaximized;

        /// <summary>
        /// 当前最大化的窗口
        /// </summary>
        public HWindowControl CurrentMaximizedWindow => currentMaxWindow;

        /// <summary>
        /// 获取所有注册的HWindowControl
        /// </summary>
        public IReadOnlyList<HWindowControl> RegisteredWindows => hWindowControls.AsReadOnly();

        /// <summary>
        /// 初始化控制器
        /// </summary>
        public TableLayoutHalconDispZoom(TableLayoutPanel tableLayoutPanel)
        {
            this.tableLayoutPanel = tableLayoutPanel ??
                throw new ArgumentNullException(nameof(tableLayoutPanel));

            // 确保TableLayoutPanel有正确的样式
            EnsureLayoutStyles();
        }

        /// <summary>
        /// 确保TableLayoutPanel有正确的行列样式
        /// </summary>
        private void EnsureLayoutStyles()
        {
            int rowCount = tableLayoutPanel.RowCount;
            int colCount = tableLayoutPanel.ColumnCount;

            // 确保有足够的行样式
            while (tableLayoutPanel.RowStyles.Count < rowCount)
            {
                tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / rowCount));
            }

            // 确保有足够的列样式
            while (tableLayoutPanel.ColumnStyles.Count < colCount)
            {
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / colCount));
            }
        }

        /// <summary>
        /// 注册HWindowControl控件
        /// </summary>
        public void RegisterWindow(HWindowControl windowControl)
        {
            if (windowControl == null || hWindowControls.Contains(windowControl))
                return;

            hWindowControls.Add(windowControl);

            // 绑定MouseDown事件而不是Click事件
            windowControl.MouseDown += WindowControl_MouseDown;

            // 也可以绑定DoubleClick事件（如果可用）
            try
            {
                windowControl.DoubleClick += WindowControl_DoubleClick;
            }
            catch
            {
                // 有些版本的HWindowControl可能不支持DoubleClick事件
            }

            if (ShowHandCursor)
            {
                windowControl.Cursor = Cursors.Hand;
            }
        }

        /// <summary>
        /// 批量注册HWindowControl控件
        /// </summary>
        public void RegisterWindows(params HWindowControl[] windows)
        {
            foreach (var window in windows)
            {
                RegisterWindow(window);
            }
        }

        /// <summary>
        /// 从TableLayoutPanel中自动注册所有HWindowControl
        /// </summary>
        public void AutoRegisterWindowsFromTable()
        {
            // 递归查找所有HWindowControl
            var allWindows = FindHWindowControls(tableLayoutPanel);

            foreach (var window in allWindows)
            {
                RegisterWindow(window);
            }
        }

        /// <summary>
        /// 递归查找所有HWindowControl
        /// </summary>
        private List<HWindowControl> FindHWindowControls(Control container)
        {
            var result = new List<HWindowControl>();

            foreach (Control control in container.Controls)
            {
                if (control is HWindowControl hWindow)
                {
                    result.Add(hWindow);
                }

                // 递归查找子控件
                if (control.HasChildren)
                {
                    result.AddRange(FindHWindowControls(control));
                }
            }

            return result;
        }

        /// <summary>
        /// 取消注册HWindowControl
        /// </summary>
        public void UnregisterWindow(HWindowControl windowControl)
        {
            if (windowControl == null) return;

            windowControl.MouseDown -= WindowControl_MouseDown;

            try
            {
                windowControl.DoubleClick -= WindowControl_DoubleClick;
            }
            catch
            {
                // 忽略异常
            }

            hWindowControls.Remove(windowControl);
        }

        /// <summary>
        /// 保存当前布局
        /// </summary>
        public void SaveCurrentLayout()
        {
            originalRowHeights.Clear();
            originalColumnWidths.Clear();

            // 保存行样式
            for (int i = 0; i < tableLayoutPanel.RowStyles.Count; i++)
            {
                originalRowHeights[i] = (int)tableLayoutPanel.RowStyles[i].Height;
            }

            // 保存列样式
            for (int i = 0; i < tableLayoutPanel.ColumnStyles.Count; i++)
            {
                originalColumnWidths[i] = (int)tableLayoutPanel.ColumnStyles[i].Width;
            }
        }

        /// <summary>
        /// 处理MouseDown事件
        /// </summary>
        private void WindowControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (sender is HWindowControl windowControl)
            {
                // 触发单击事件
                OnWindowClicked?.Invoke(this, windowControl);

                // 检查是否为双击
                var now = DateTime.Now;
                bool isDoubleClick = (windowControl == lastClickedWindow &&
                                     (now - lastClickTime).TotalMilliseconds < DoubleClickInterval);

                if (isDoubleClick)
                {
                    // 双击事件
                    OnWindowDoubleClicked?.Invoke(this, windowControl);

                    if (MaximizeOnDoubleClick)
                    {
                        ToggleWindowMaximize(windowControl);
                    }
                }
                else
                {
                    // 单击事件
                    if (MaximizeOnSingleClick)
                    {
                        ToggleWindowMaximize(windowControl);
                    }
                }

                // 更新最后点击时间
                lastClickTime = now;
                lastClickedWindow = windowControl;
            }
        }

        /// <summary>
        /// 处理DoubleClick事件
        /// </summary>
        private void WindowControl_DoubleClick(object sender, EventArgs e)
        {
            if (sender is HWindowControl windowControl)
            {
                // 双击事件
                OnWindowDoubleClicked?.Invoke(this, windowControl);

                if (MaximizeOnDoubleClick)
                {
                    ToggleWindowMaximize(windowControl);
                }
            }
        }

        /// <summary>
        /// 切换窗口最大化状态
        /// </summary>
        private void ToggleWindowMaximize(HWindowControl windowControl)
        {
            if (isMaximized && currentMaxWindow == windowControl)
            {
                // 如果当前已最大化且点击的是同一个窗口
                if (EnableClickToRestore)
                {
                    RestoreLayout();
                }
            }
            else
            {
                // 最大化新窗口
                MaximizeWindow(windowControl);
            }
        }

        /// <summary>
        /// 最大化指定的Halcon窗口
        /// </summary>
        public void MaximizeWindow(HWindowControl windowControl)
        {
            if (windowControl == null) return;

            // 如果已最大化但不是同一个窗口，先恢复
            if (isMaximized && windowControl != currentMaxWindow)
            {
                RestoreLayout();
            }

            // 保存布局（如果是第一次最大化）
            if (originalRowHeights.Count == 0)
            {
                SaveCurrentLayout();
            }

            // 获取窗口在TableLayoutPanel中的位置
            int row = tableLayoutPanel.GetRow(windowControl);
            int column = tableLayoutPanel.GetColumn(windowControl);

            if (row < 0 || column < 0) return;

            // 最大化指定的单元格
            MaximizeCell(row, column);

            // 更新状态
            isMaximized = true;
            currentMaxWindow = windowControl;

            // 触发最大化事件
            OnWindowMaximized?.Invoke(this, windowControl);
        }

        /// <summary>
        /// 最大化单元格
        /// </summary>
        private void MaximizeCell(int row, int column)
        {
            // 隐藏所有行
            for (int i = 0; i < tableLayoutPanel.RowStyles.Count; i++)
            {
                tableLayoutPanel.RowStyles[i].Height = 0;
            }

            // 隐藏所有列
            for (int i = 0; i < tableLayoutPanel.ColumnStyles.Count; i++)
            {
                tableLayoutPanel.ColumnStyles[i].Width = 0;
            }

            // 显示选中的行
            if (row < tableLayoutPanel.RowStyles.Count)
            {
                tableLayoutPanel.RowStyles[row].Height = 100;
            }

            // 显示选中的列
            if (column < tableLayoutPanel.ColumnStyles.Count)
            {
                tableLayoutPanel.ColumnStyles[column].Width = 100;
            }
        }

        /// <summary>
        /// 最大化指定位置
        /// </summary>
        public void MaximizePosition(int row, int column)
        {
            if (row < 0 || row >= tableLayoutPanel.RowCount ||
                column < 0 || column >= tableLayoutPanel.ColumnCount)
            {
                throw new ArgumentOutOfRangeException("行或列索引超出范围");
            }

            // 查找该位置的HWindowControl
            HWindowControl targetWindow = null;
            foreach (Control control in tableLayoutPanel.Controls)
            {
                int r = tableLayoutPanel.GetRow(control);
                int c = tableLayoutPanel.GetColumn(control);

                if (r == row && c == column && control is HWindowControl hWindow)
                {
                    targetWindow = hWindow;
                    break;
                }
            }

            if (targetWindow != null)
            {
                MaximizeWindow(targetWindow);
            }
            else
            {
                // 如果没有HWindowControl，直接最大化单元格
                if (isMaximized) RestoreLayout();

                if (originalRowHeights.Count == 0)
                {
                    SaveCurrentLayout();
                }

                MaximizeCell(row, column);
                isMaximized = true;
                currentMaxWindow = null;
            }
        }

        /// <summary>
        /// 恢复原始布局
        /// </summary>
        public void RestoreLayout()
        {
            if (!isMaximized) return;

            // 恢复行样式
            foreach (var kvp in originalRowHeights)
            {
                if (kvp.Key < tableLayoutPanel.RowStyles.Count)
                {
                    tableLayoutPanel.RowStyles[kvp.Key].Height = kvp.Value;
                }
            }

            // 恢复列样式
            foreach (var kvp in originalColumnWidths)
            {
                if (kvp.Key < tableLayoutPanel.ColumnStyles.Count)
                {
                    tableLayoutPanel.ColumnStyles[kvp.Key].Width = kvp.Value;
                }
            }

            // 更新状态
            isMaximized = false;
            currentMaxWindow = null;

            // 触发恢复事件
            OnLayoutRestored?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// 获取指定位置的HWindowControl
        /// </summary>
        public HWindowControl GetWindowAtPosition(int row, int column)
        {
            foreach (Control control in tableLayoutPanel.Controls)
            {
                int r = tableLayoutPanel.GetRow(control);
                int c = tableLayoutPanel.GetColumn(control);

                if (r == row && c == column && control is HWindowControl hWindow)
                {
                    return hWindow;
                }
            }
            return null;
        }

        /// <summary>
        /// 强制刷新所有窗口
        /// </summary>
        public void RefreshAllWindows()
        {
            foreach (var window in hWindowControls)
            {
                try
                {
                    window.Refresh();
                }
                catch
                {
                    // 忽略刷新错误
                }
            }
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            foreach (var window in hWindowControls)
            {
                window.MouseDown -= WindowControl_MouseDown;

                try
                {
                    window.DoubleClick -= WindowControl_DoubleClick;
                }
                catch
                {
                    // 忽略异常
                }
            }

            hWindowControls.Clear();
            originalRowHeights.Clear();
            originalColumnWidths.Clear();

            tableLayoutPanel = null;
            currentMaxWindow = null;
        }

    }
}
