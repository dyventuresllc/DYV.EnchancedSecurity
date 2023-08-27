using System.Text;

namespace DYV.EnchancedSecurity.Agent.Helpers
{
    public static class MessageHandler
    {
        const string NoGroupGridHeader = "<style type=\"text/css\">table, th, td {\r\n  border: 1px solid black;\r\n  border-collapse: collapse;\r\n}\r\n</style>\r\n<p><br />\r\n<b><i style=\"color:red;\">Disabled Users (users with no groups): </i></b></p>\r\n\r\n<table style=\"width:80%\">\r\n\t<tbody>\r\n\t\t<tr>\r\n\t\t\t<th>First Name</th>\r\n\t\t\t<th>Last Name</th>\r\n\t\t\t<th>Email Address</th>\r\n\t\t</tr>\r\n";
        const string NoGroupGridFooter = "</tbody>\r\n</table>";

        public static StringBuilder Gridbody(string nameFirst, string nameLast, string emailAddress, StringBuilder gridbody) => gridbody.Append($"<tr>\r\n\t\t\t<th>{nameFirst}</th>\r\n\t\t\t<th>{nameLast}</th>\r\n\t\t\t<th>{emailAddress}</th>\r\n\t\t</tr>");

        public static string MesssageNoGroups(StringBuilder gridBody) =>  string.Concat(NoGroupGridHeader, gridBody, NoGroupGridFooter);       
    }
}
