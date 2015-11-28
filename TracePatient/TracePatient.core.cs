using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TracePatient
{
    public partial class TracePatientForm
    {
        private void ReconfigWardName(string newWardName)
        {
            iniFile.WardName = newWardName;
            txtWardNO.Text = iniFile.WardName;

            try
            {
                GlobalConfigurationAndSetup.ReconfigIniFile(iniFile);
            }
            catch (ArgumentNullException exception)
            {
                GlobalConfigurationAndSetup.TraceLogger.TraceLog("reset ward name failed", this.GetType(), exception);
                GlobalConfigurationAndSetup.TraceLogger.TraceConsole("设置检查室失败");
            }

            GlobalConfigurationAndSetup.TraceLogger.TraceConsole("检查室设置成功");
        }
    }
}
