using System;
using System.Collections.Generic;

namespace HelperLibrary.Models
{
    public class CommonModel
    {
        public CommonModel(int grade, string title,DateTime date, string stFirstName, string stLastName, string leFirstName, string leLastName)
        {
            Grade = grade;
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Date = date;
            Le_First_Name = leFirstName ?? throw new ArgumentNullException(nameof(leFirstName));
            Le_Last_Name = leLastName ?? throw new ArgumentNullException(nameof(leLastName));
            St_First_Name = stFirstName ?? throw new ArgumentNullException(nameof(stFirstName));
            St_Last_Name = stLastName ?? throw new ArgumentNullException(nameof(stLastName));
        }

        public CommonModel() {}
        public int Grade { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Le_First_Name { get; set; }
        public string Le_Last_Name { get; set; }
        public string St_First_Name { get; set; }
        public string St_Last_Name { get; set; }
        public string FullInfoForStudent => $"Date: {Date}, Grade: {Grade}";
        
        public string FullInfoForLection => $"Lecturer: {Le_Last_Name} {Le_First_Name}, Subject: {Title}";
    }
}