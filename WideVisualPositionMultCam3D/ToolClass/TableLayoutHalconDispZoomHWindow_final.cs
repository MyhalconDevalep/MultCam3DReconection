using ChoiceTech.Halcon.Control;
using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WideVisualPositionMultCam3D.ToolClass
{


    public class TableLayoutHalconDispZoomHWindow_final
    {
        private readonly TableLayoutPanel table;
        private readonly List<HWindow_Final> windows = new List<HWindow_Final>();
        private readonly Dictionary<HWindowControl, HWindow_Final> map = new Dictionary<HWindowControl, HWindow_Final>();

        private readonly Dictionary<int, float> rowBackup = new Dictionary<int, float>();
        private readonly Dictionary<int, float> colBackup = new Dictionary<int, float>();

        private bool isMaximized = false;
        private HWindow_Final currentMax = null;

        private DateTime lastClickTime = DateTime.MinValue;
        private HWindow_Final lastClickWin = null;
        private const int DoubleClickInterval = 300;

        // ===== 防抖刷新相关 =====
        private CancellationTokenSource sizeChangedCts;



        public TableLayoutHalconDispZoomHWindow_final(TableLayoutPanel panel)
        {
            table = panel;
        }

        #region 注册

        public void Register(HWindow_Final win)
        {
            if (win == null || win.hWindowControl == null) return;

            if (map.ContainsKey(win.hWindowControl)) return;

            windows.Add(win);
            map[win.hWindowControl] = win;

            // 只注册鼠标双击逻辑
            win.hWindowControl.MouseDown += OnMouseDown;

            // 尺寸变化 → 防抖异步刷新
            win.hWindowControl.SizeChanged += OnHalconSizeChanged;
        }

        #endregion

        #region 防抖异步刷新 SizeChanged


        private readonly Dictionary<HWindow_Final, CancellationTokenSource> sizeChangedTokens = new Dictionary<HWindow_Final, CancellationTokenSource>();

        private void OnHalconSizeChanged(object sender, EventArgs e)
        {
            if (!(sender is HWindowControl h) || !map.TryGetValue(h, out var win))
                return;

            // 取消上一次任务（如果存在）
            if (sizeChangedTokens.TryGetValue(win, out var oldCts))
            {
                oldCts.Cancel();
                oldCts.Dispose();
            }

            var cts = new CancellationTokenSource();
            sizeChangedTokens[win] = cts;
            var token = cts.Token;

            Task.Run(async () =>
            {
                try
                {
                    await Task.Delay(1000, token);
                    if (token.IsCancellationRequested) return;

                    win.hWindowControl.Invoke(new Action(() =>
                    {
                        try
                        {
                            win.viewWindow.resetWindowImage();
                        }
                        catch { }
                    }));

                    // 完成刷新后移除 token
                    sizeChangedTokens.Remove(win);
                }
                catch (TaskCanceledException) { }
            });
        }



        #endregion

        #region 双击入口（两套）

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (!(sender is HWindowControl h)) return;
            if (!map.TryGetValue(h, out var win)) return;

            var now = DateTime.Now;
            if (lastClickWin == win && (now - lastClickTime).TotalMilliseconds < DoubleClickInterval)
                Toggle(win);

            lastClickTime = now;
            lastClickWin = win;
        }

        #endregion

        #region 放大 / 还原 TableLayoutPanel

        private void Toggle(HWindow_Final win)
        {
            if (isMaximized && currentMax == win)
                Restore();
            else
                Maximize(win);
        }

        private void Maximize(HWindow_Final win)
        {
        
            if (!isMaximized)
                BackupLayout();

            int row = table.GetRow(win);
            int col = table.GetColumn(win);

            table.SuspendLayout();

            for (int i = 0; i < table.RowStyles.Count; i++)
                table.RowStyles[i].Height = 0;

            for (int i = 0; i < table.ColumnStyles.Count; i++)
                table.ColumnStyles[i].Width = 0;

            table.RowStyles[row].Height = 100;
            table.ColumnStyles[col].Width = 100;

            table.ResumeLayout(true);

            isMaximized = true;
            currentMax = win;
      
        }

        private void Restore()
        {
      
            table.SuspendLayout();

            foreach (var r in rowBackup)
                table.RowStyles[r.Key].Height = r.Value;

            foreach (var c in colBackup)
                table.ColumnStyles[c.Key].Width = c.Value;

            table.ResumeLayout(true);

            isMaximized = false;
            currentMax = null;
          
        }



        private void BackupLayout()
        {
            rowBackup.Clear();
            colBackup.Clear();

            for (int i = 0; i < table.RowStyles.Count; i++)
                rowBackup[i] = table.RowStyles[i].Height;

            for (int i = 0; i < table.ColumnStyles.Count; i++)
                colBackup[i] = table.ColumnStyles[i].Width;
        }

        #endregion
    }
}

