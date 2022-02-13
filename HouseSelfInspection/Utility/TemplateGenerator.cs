using HouseSelfInspection.Controllers;
using HouseSelfInspection.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseSelfInspection.Utility
{
    public class TemplateGenerator
    {
        private readonly ApplicationContext _context;

        public TemplateGenerator(ApplicationContext context)
        {
            _context = context;
        }


        public string GetHTMLString()
        {
            var inspections = _context.InspectionSchedules.ToList<InspectionScheduleModel>();
            var sb = new StringBuilder();
            sb.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><h1>This is the generated PDF report!!!</h1></div>
                                <table align='center'>
                                    <tr>
                                        <th>House</th>
                                        <th>Tenant</th>
                                        <th>Date</th>
                                    </tr>");
            foreach (var inspection in inspections)
            {
                sb.AppendFormat(@"<tr>
                                    <td>{0}</td>
                                    <td>{1}</td>
                                    <td>{2}</td>
                                  </tr>", inspection.HouseId, inspection.UserId, inspection.Inspection_date);
            }
            sb.Append(@"
                                </table>
                            </body>
                        </html>");
            return sb.ToString();
        }
    }
}
