using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;

namespace ProxyClient.Controls
{
    /// <summary>
    /// 内镜清洗状态
    /// </summary>
    public partial class EndoscopeState : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EndoscopeState"/> class.
        /// </summary>
        /// <param name="endoscopeSN">The endoscope sn.</param>
        public EndoscopeState(string endoscopeSN)
        {
            InitializeComponent();
            this.cleanType = 1;
            this.endoscopeLabel.Text = endoscopeSN + "清洗状态";
        }

        /// <summary>
        /// Gets or sets the type of the clean.
        /// </summary>
        /// <value>
        /// The type of the clean.
        /// </value>
        public Byte cleanType { get; set; }

        /// <summary>
        /// Gets or sets the first wash begin.
        /// </summary>
        /// <value>
        /// The first wash begin.
        /// </value>
        public DateTime? FirstWashBegin { get; set; }

        /// <summary>
        /// Gets or sets the first wash end.
        /// </summary>
        /// <value>
        /// The first wash end.
        /// </value>
        public DateTime? FirstWashEnd { get; set; }


        /// <summary>
        /// Gets or sets the first wash time.
        /// </summary>
        /// <value>
        /// The first wash time.
        /// </value>
        public double FirstWashTime { get; set; }

        /// <summary>
        /// Gets or sets the brush wash begin.
        /// </summary>
        /// <value>
        /// The brush wash begin.
        /// </value>
        public DateTime? BrushWashBegin { get; set; }

        /// <summary>
        /// Gets or sets the brush wash end.
        /// </summary>
        /// <value>
        /// The brush wash end.
        /// </value>
        public DateTime? BrushWashEnd { get; set; }

        /// <summary>
        /// Gets or sets the brush wash time.
        /// </summary>
        /// <value>
        /// The brush wash time.
        /// </value>
        public double BrushWashTime { get; set; }

        /// <summary>
        /// Gets or sets the ezyme wash begin.
        /// </summary>
        /// <value>
        /// The ezyme wash begin.
        /// </value>
        public DateTime? EzymeWashBegin { get; set; }

        /// <summary>
        /// Gets or sets the ezyme wash end.
        /// </summary>
        /// <value>
        /// The ezyme wash end.
        /// </value>
        public DateTime? EzymeWashEnd { get; set; }

        /// <summary>
        /// Gets or sets the ezyme wash time.
        /// </summary>
        /// <value>
        /// The ezyme wash time.
        /// </value>
        public double EzymeWashTime { get; set; }

        /// <summary>
        /// Gets or sets the clean out begin.
        /// </summary>
        /// <value>
        /// The clean out begin.
        /// </value>
        public DateTime? CleanOutBegin { get; set; }

        /// <summary>
        /// The clean out end
        /// </summary>
        public DateTime? CleanOutEnd { get; set; }

        /// <summary>
        /// Gets or sets the clean out time.
        /// </summary>
        /// <value>
        /// The clean out time.
        /// </value>
        public double CleanOutTime { get; set; }

        /// <summary>
        /// Gets or sets the dip information begin.
        /// </summary>
        /// <value>
        /// The dip information begin.
        /// </value>
        public DateTime? DipInBegin { get; set; }

        /// <summary>
        /// Gets or sets the dip information end.
        /// </summary>
        /// <value>
        /// The dip information end.
        /// </value>
        public DateTime? DipInEnd { get; set; }

        /// <summary>
        /// Gets or sets the dip information time.
        /// </summary>
        /// <value>
        /// The dip information time.
        /// </value>
        public double DipInTime { get; set; }

        /// <summary>
        /// Gets or sets the last wash begin.
        /// </summary>
        /// <value>
        /// The last wash begin.
        /// </value>
        public DateTime? LastWashBegin { get; set; }

        /// <summary>
        /// Gets or sets the last wash end.
        /// </summary>
        /// <value>
        /// The last wash end.
        /// </value>
        public DateTime? LastWashEnd { get; set; }

        /// <summary>
        /// Gets or sets the last wash time.
        /// </summary>
        /// <value>
        /// The last wash time.
        /// </value>
        public double LastWashTime { get; set; }

        /// <summary>
        /// Gets or sets the dip information sec begin.
        /// </summary>
        /// <value>
        /// The dip information sec begin.
        /// </value>
        public DateTime? DipInSecBegin { get; set; }

        /// <summary>
        /// Gets or sets the dip information sec end.
        /// </summary>
        /// <value>
        /// The dip information sec end.
        /// </value>
        public DateTime? DipInSecEnd { get; set; }

        /// <summary>
        /// Gets or sets the dip information sec time.
        /// </summary>
        /// <value>
        /// The dip information sec time.
        /// </value>
        public double DipInSecTime { get; set; }

        /// <summary>
        /// Gets or sets the last sec wash begin.
        /// </summary>
        /// <value>
        /// The last sec wash begin.
        /// </value>
        public DateTime? LastSecWashBegin { get; set; }

        /// <summary>
        /// Gets or sets the last sec wash end.
        /// </summary>
        /// <value>
        /// The last sec wash end.
        /// </value>
        public DateTime? LastSecWashEnd { get; set; }

        /// <summary>
        /// Gets or sets the last sec wash time.
        /// </summary>
        /// <value>
        /// The last sec wash time.
        /// </value>
        public double LastSecWashTime { get; set; }

        /// <summary>
        /// Gets or sets the endoscope clean standar data.
        /// </summary>
        /// <value>
        /// The endoscope clean standar data.
        /// </value>
        public string EndoscopeCleanStandarData { get; set; }

        private void contentEndoscope_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtEndoscopeID_TextChanged(object sender, EventArgs e)
        {
            this.mainGb.Values.Heading = this.txtEndoscopeID.Text.Trim();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }


    }
}
