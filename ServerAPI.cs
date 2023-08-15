using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Nerdogramm
{
    public class ServerAPI
    {
    public class Organisation
    {
        public SolidColorBrush colorBrush { get; set; }
        public int id { get; set; }
        public String name { get; set; }
    }

   

    public class Message
    {
        public int feedback { get; set; }
        public String text { get; set; }

        public Message(int feedback, String text)
        {
            this.feedback = feedback;
            this.text = text;
        }
    }

    public class GroupInfo
    {
        public String description { get; set; }
        public List<Message> messages { get; set; }

        public GroupInfo()
        {
            messages = new List<Message>();
        }
        
    }

    static private String arrtostr1(String[] a)
    {
        String ret = "";
        foreach (String s in a)
            ret += s.Replace("\\", "\\\\").Replace(",", "\\,") + ',';
        if (ret.EndsWith(","))
            ret = ret.Substring(0, ret.Length - 1);
        return ret;
    }

    static private String[] strtoarr1(String a)
    {
        int cou = 1;
        for (int i = 0; i < a.Length; i++)
            if (a[i] == '\\')
                i++;
            else if (a[i] == ',')
                cou++;
        String[] ret = new String[cou];
        for (int i = 0; i < ret.Length; i++)
            ret[i] = "";
        cou = 0;
        for (int i = 0; i < a.Length; i++)
            if (a[i] == '\\')
            {
                i++;
                ret[cou] += a[i];
            }
            else if (a[i] == ',')
                cou++;
            else
                ret[cou] += a[i];
        return ret;
    }

    static private String[] sendRequest(String[] a)
    {
        try
        {
            WebRequest request = WebRequest.Create("https://nerdogramm.000webhostapp.com/test.php?c=" + arrtostr1(a));
            request.Credentials = CredentialCache.DefaultCredentials;
            request.Timeout = 5000;
            WebResponse response = request.GetResponse();
            using (Stream dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                response.Close();
                return strtoarr1(responseFromServer);
            }
        }
        catch (Exception e) { }
        return null;
    }

    public bool ping()
    {
        String[] resp = sendRequest(new String[] { "ping" });
        if (resp == null)
            return false;
        return resp[0] == "pong";
    }

    public int register(String name, String login, String password)
    {
        String[] resp = sendRequest(new String[] { "register", name, login, password });
        return resp[0] == "error" ? -1 : int.Parse(resp[0]);
    }

    public String login(String login, String password)
    {
        String[] resp = sendRequest(new String[] { "login", login, password });
        return resp[0] == "error" ? null : resp[0];
    }

    public List<Organisation> getOrganisations()
    {
        String[] resp = sendRequest(new String[] { "get_organisations" });
        List<Organisation> ret = new List<Organisation>();
        for (int i = 0; i < resp.Length; i += 2)
        {
            Organisation org = new Organisation();
            org.id = int.Parse(resp[i]);
            org.name = resp[i + 1];
            ret.Add(org);
        }
        return ret;
    }

    public List<Group> getGroups(int organisationId)
    {
        String[] resp = sendRequest(new String[] { "get_groups", organisationId.ToString() });
        List<Group> ret = new List<Group>();
        int i = 0;
        if (resp.Length > 0 && resp[0] != "")
            while (i < resp.Length)
            {
                Group ng = new Group();
                ng.id = int.Parse(resp[i]);
                ng.header = resp[i + 1];
                int tag_count = int.Parse(resp[i + 2]);
                i += 3;
                for (int i1 = 0; i1 < tag_count; i1++)
                {
                    ng.tags.Add(new Tag(int.Parse(resp[i]), resp[i + 1]));
                    i += 2;
                }
                ret.Add(ng);
            }
        return ret;
    }

    public GroupInfo getGroupInfo(String tempKey, int groupId)
    {
        String[] resp = sendRequest(new String[] { "get_group_info", tempKey, groupId.ToString() });
            if (resp.Length == 0 || resp[0] == "not_enough_points")
                return null;
        GroupInfo ret = new GroupInfo();
        ret.description = resp[0];
        int msg_count = int.Parse(resp[1]);
        for (int i = 0; i < msg_count; i++)
            ret.messages.Add(new Message(int.Parse(resp[2 + i * 2]), resp[3 + i * 2]));
        return ret;
    }

        public class UserInfo
        {
            public String name { get; set; }
            public String login { get; set; }
            public List<Tag> tags { get; set; }
                
            public UserInfo()
            {
                tags = new List<Tag>();
            }
        }
        public UserInfo getUserInfo(String tempKey)
        {
            String[] resp = sendRequest(new String[] { "get_user_info", tempKey });
            if (resp.Length == 0)
                return null;
            UserInfo cui = new UserInfo();
            cui.name = resp[0];
            cui.login = resp[1];
            int tags_count = int.Parse(resp[2]);
            for (int i = 0; i < tags_count; i++)
                cui.tags.Add(new Tag(int.Parse(resp[3 + i*2]), resp[4 + i*2]));
            return cui;
        }
    }
}
