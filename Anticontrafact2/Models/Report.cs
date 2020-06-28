using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Anticontrafact2.Models
{
    public enum ReportStatus
    {
        inProcessing,
        ready

    }
    public class Report
    {

        public string TitleName { get; set; }
        public string Address { get; set; }
        public string Number { get; set; }
        public string Description { get; set; }
        public ReportStatus State { get; set; }
        public string CommentAboutReport { get; set; }

        public string Inform
        {
            get
            {
                return TitleName + "\n" + Address + "\n" + Number;
            }
        }
        public string StatusOnPage
        {
            get
            {
                if (State == ReportStatus.inProcessing)
                    return "В обработке";
                else
                    return "Рассмотрено";

            }
        }
        public Color ColorTextOnPage
        {
            get
            {
                if (State == ReportStatus.inProcessing)
                    return Color.Yellow;
                else
                    return Color.Green;
            }
        }

    }
}
