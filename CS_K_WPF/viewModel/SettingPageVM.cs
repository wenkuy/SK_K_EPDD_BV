using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_K_WPF.viewModel
{
    public partial class SettingPageVM : ObservableObject
    {
        [ObservableProperty]
        private float angle;

        public SettingPageVM()
        {
            SettintAngle();
        }

        private void SettintAngle()
        {
            bool bl = false;

            Task.Run(async () =>
            {
                while (true)
                {
                    if (bl)
                    {

                        Angle = (Angle + 1);  //范围-90 ~0
                    }
                    else
                    {
                        Angle = (Angle - 1);
                    }


                   await Task.Delay(50);

                    if (Angle >= 0)
                    {
                        Angle = 0; // 强制拉回上限，防止超界
                        bl = false; // 切换为递减方向（往-90走）
                    }
                    else if (Angle <= -90)
                    {
                        Angle = -90; // 强制拉回下限，防止超界
                        bl = true; // 切换为递增方向（往0走）
                    }
                }
            });
        }
    }
}
