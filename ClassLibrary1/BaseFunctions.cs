using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Configuration;
using System.Net.Mail;
using System.Net;

namespace ISBusinessLayer
{
    public static class BaseFunctions
    {

        public static bool isDemo
        {
            get
            {
                return Convert.ToBoolean(ConfigurationSettings.AppSettings["demo"]) ;
            }
        }
        public static String basePath
        {
            get
            {
                return ConfigurationSettings.AppSettings["basePath"];
            }
        }

        internal static String storePath
        {
            get
            {
                /*string path;
                path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
                path = new Uri(path).LocalPath;*/
                
               // return path + "\\..\\..\\StorePath\\";
                return ConfigurationSettings.AppSettings["storePath"];
            }
        }

        internal static SqlConnection getConnection()
        {

            try
            {

                string connectionString = ConfigurationSettings.AppSettings["sqlConnection"];
                //@"Server=localhost\SQLEXPRESS;Database=imageDB;Trusted_Connection=True;";

                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                return connection;
            }
            catch (Exception ex)
            {
                // Log what you need from here.
                throw new Exception("1");
            }
        }



        internal static List<R> selectMethod<C, R>(C caller, int mode, string byId= null, string additional=null)
        {
            List<R> retList= new List<R>();
            string callerName = typeof(C).Name;
            string requestedName = typeof(R).Name;
            FieldInfo[] fi = typeof(R).GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
            //sql Abfrage
            string cmd="";
            string fields = "";
            string callerID=typeof(C).GetProperty("ID").GetValue(caller).ToString();
            int fieldCount = fi.Count();

        foreach (FieldInfo singleFi in fi)
       {
             if(singleFi.Name.Substring(0,1)=="_")
             {
                 fields+=requestedName + "." + singleFi.Name.Substring(1) + ",";
             }
         }
            fields=fields.Substring(0,fields.Length-1);
             string joinTable;
            switch (mode)
            {
                   
                    //select by join table -> caller is bigger requested is smaller
                case 1:
                   joinTable= requestedName + "To" + callerName;
                   //cmd="Select " + fields + " from " + requestedName + " inner join " + joinTable +" on " + joinTable + "." + requestedName + "ID=" + requestedName + "." + "ID" + " where (" + joinTable + "." + callerName + "ID='" + callerID  +"'";
                   cmd = "Select " + fields + " from " + requestedName + " left outer join " + joinTable + " on " + joinTable + "." + requestedName + "ID=" + requestedName + "." + "ID" + " where (" + joinTable + "." + callerName + "ID IS NOT NULL AND " + joinTable + "." + callerName + "ID='" + callerID + "'";
                    break;

                    //select by join table -> caller is smaller requested is bigger
                case 2:
                   joinTable= callerName + "To" + requestedName;
                   //cmd="Select " + fields + " from " + requestedName + " inner join " + joinTable +" on " + joinTable + "." + requestedName + "ID=" + requestedName + "." + "ID" + " where (" + joinTable + "." + callerName + "ID='" + callerID  +"'";
                   cmd = "Select " + fields + " from " + requestedName + " left outer join " + joinTable + " on " + joinTable + "." + requestedName + "ID=" + requestedName + "." + "ID" + " where  (" + joinTable + "." + callerName + "ID IS NOT NULL AND " + joinTable + "." + callerName + "ID='" + callerID + "'";
                    break;

                    //select from one to many (images to folder  --> no join. just by id of folder)
                case 3:
                    cmd="Select " + fields + " from " + requestedName + " where (" + requestedName + "." + callerName + "ID='" + callerID +"'";
                    break;

                    //select recursive from many to one (folder to image)
                case 4:
                    cmd="Select " + fields + " from " + requestedName + " inner join " + callerName + " on " + callerName + "." + requestedName + "ID=" + requestedName + "." + "ID" + " where (" + callerName + "." + "ID='" + callerID + "'";
                    break;

                    //specfic select by id (needs to set byID or simply select ALL!
                case 5:
                    cmd="Select " + fields + " from " + requestedName + " where (" + requestedName + ".ID IS NOT NULL";
                    break;
            }
            String cmdid = "";
            if (byId!=null)
            {
                cmdid = " AND " + requestedName + ".ID='" + byId + "'";
            }
            cmd += cmdid;
            cmd += ")";
            if (additional!=null)
            {
                cmd+=" " + additional + cmdid;
            }
              SqlCommand SQLcmd = new SqlCommand();
                SQLcmd.CommandText = cmd;
                try
                {
                    SQLcmd.Connection = getConnection();
                }
                catch (Exception e)
                {
                    throw e;
                }

            SqlDataReader ergebnis=SQLcmd.ExecuteReader();
            if (!ergebnis.HasRows)
            {
                throw new Exception("0");
            }
            while (ergebnis.Read())
            {
                R tmpObj=(R)Activator.CreateInstance(typeof(R),BindingFlags.Instance  | BindingFlags.NonPublic,null,null,null);
                typeof(R).GetProperty("hasManagementAccess").SetValue(tmpObj, typeof(C).GetProperty("hasManagementAccess").GetValue(caller));

                foreach (FieldInfo singleFi in fi)
                {
                    if(singleFi.Name.Substring(0,1)=="_")
                    {
                        string zz = singleFi.FieldType.Name;//typeof(R).GetProperty(singleFi.Name.Substring(1)).PropertyType.Name;
                        switch (zz.ToLower())
                        {
                            case "string":
                                singleFi.SetValue(tmpObj, ergebnis.GetString(ergebnis.GetOrdinal(singleFi.Name.Substring(1))));
                            break;
                            case "int":
                                singleFi.SetValue(tmpObj, ergebnis.GetInt32(ergebnis.GetOrdinal(singleFi.Name.Substring(1))));
                            break;
                            case "int32":
                                 singleFi.SetValue(tmpObj, ergebnis.GetInt32(ergebnis.GetOrdinal(singleFi.Name.Substring(1))));
                            break;
                            case "int64":
                                 singleFi.SetValue(tmpObj, ergebnis.GetInt32(ergebnis.GetOrdinal(singleFi.Name.Substring(1))));
                            break;
                            case "guid":
                                 singleFi.SetValue(tmpObj, ergebnis.GetGuid(ergebnis.GetOrdinal(singleFi.Name.Substring(1))));
                            break;
                            case "float":
                                 singleFi.SetValue(tmpObj, ergebnis.GetFloat(ergebnis.GetOrdinal(singleFi.Name.Substring(1))));
                            break;
                            case "single":
                                 singleFi.SetValue(tmpObj, (float)ergebnis.GetDouble(ergebnis.GetOrdinal(singleFi.Name.Substring(1))));
                            break;
                            case "double":
                                typeof(R).GetProperty(singleFi.Name.Substring(1)).SetValue(tmpObj, ergebnis.GetDouble(ergebnis.GetOrdinal(singleFi.Name.Substring(1))));
                            break;
                        }
                    }
                }
                    retList.Add(tmpObj);
                 /*   List<R> ff=new List<R>();
                    ff.Add(tmpObj);
                typeof(R).GetMethod("fwe").Invoke(tmpObj,
                    ((BaseObject)tmpObj).hierachy.Add(ff);*/
              
            }
                        
            if(retList.Count==0)
            {retList=null;}
            
           
            return retList;
}

        
        
        internal static bool updateMethod<R>(R requested, string changeval, object newValue)
        {
            if (isDemo) throw new Exception("5000");
            String iValue = newValue.ToString();
            iValue = iValue.Replace(",", ".");
            if (newValue.ToString() == "")
            {
                throw new Exception("26");
            }
            string requestedName = typeof(R).Name;
           /* PropertyInfo pi= typeof(R).GetProperty(changeval);
            string newValue=pi.GetValue(requested).ToString();*/
            string cmd = "UPDATE " + requestedName + " SET " + changeval + "='" + iValue + "' where ID='" + typeof(R).GetProperty("ID").GetValue(requested).ToString() + "'";

            SqlCommand SQLcmd = new SqlCommand();
            SQLcmd.CommandText = cmd;
            try
            {
                SQLcmd.Connection = getConnection();
            }
            catch (Exception e)
            {
                throw e;
            }
            int ret=SQLcmd.ExecuteNonQuery();
            if (ret == 0)
            {
                throw new Exception("2");
            }
            return true;
        }


        internal static R insertMethod<R>(params object[] newVals)
        {
            if (isDemo) throw new Exception("5000");
            int i = 0;
            foreach (object o in newVals)
            {
                if (o.ToString() == "")
                {
                    throw new Exception("27");
                }
               
            }
            string requestedName = typeof(R).Name;
            StackTrace st = new StackTrace();
            MethodBase meth = st.GetFrame(1).GetMethod();
            ParameterInfo[] pi= meth.GetParameters();
            string cmd = "Insert INTO " + requestedName;
            string cmd2 = "(";
            string cmd3 = "(";
            int k = 0;
            foreach (ParameterInfo singlePi in pi)
            {
                cmd2+=singlePi.Name +",";
                if (singlePi.ParameterType.Name == "float" || singlePi.ParameterType.Name == "Single" || singlePi.ParameterType.Name == "double")
                {
                    newVals[k] = (object)newVals[k].ToString().Replace("," , ".");
                }
                k++;
            }
            cmd2 = cmd2.Substring(0, cmd2.Length - 1);
            foreach (object singleObj in newVals)
            {
                cmd3 += "'" + singleObj.ToString() + "',";
            }
            cmd3 = cmd3.Substring(0, cmd3.Length - 2);
            
            
            cmd2 = cmd2 + ") OUTPUT INSERTED.ID VALUES ";
            cmd3 = cmd3 + "')";
            cmd += cmd2 + cmd3;
            SqlCommand SQLcmd = new SqlCommand();
            SQLcmd.CommandText = cmd;
            try
            {
                SQLcmd.Connection = getConnection();
            }
            catch (Exception e)
            {
                throw e;
            }
            SqlDataReader ergebnis;
            try
            {
                 ergebnis = SQLcmd.ExecuteReader();
            }
            catch (Exception e)
            {
                throw new Exception("24");
            }
            if (!ergebnis.HasRows)
            {
                throw new Exception("3");
            }
            R tmpObj=(R)Activator.CreateInstance(typeof(R),BindingFlags.Instance  | BindingFlags.NonPublic,null,null,null);

            ergebnis.Read();
            try
            {
                typeof(R).GetProperty("hasManagementAccess").SetValue(tmpObj, true);
                List<R> retVal = selectMethod<R, R>(tmpObj, 5, ergebnis.GetGuid(0).ToString());
                return retVal[0];
            }
            catch (Exception e)
            {
                
                throw e;
            }
         
        }




        internal static bool insertJoin<R,R2>(R requested1, R2 requested2,int mode)
        {
            if (isDemo) throw new Exception("5000");
            string requestedName1 = typeof(R).Name;
            string requestedName2 = typeof(R2).Name;
            string cmd = "Insert Into ";
            switch (mode)
            {
                case 1:
                    cmd +=requestedName1 + "To" + requestedName2; 
                    break;
                case 2:
                    cmd += requestedName2 + "To" + requestedName1;
                    break;
            }
            cmd += " (" + requestedName1 + "ID, " + requestedName2 + "ID)";
            cmd += " Values ('" + typeof(R).GetProperty("ID").GetValue(requested1).ToString() + "','"  + typeof(R2).GetProperty("ID").GetValue(requested2).ToString() + "')";

            SqlCommand SQLcmd = new SqlCommand();
            SQLcmd.CommandText = cmd;
                try
                {
                    SQLcmd.Connection = getConnection();
                }
                catch (Exception e)
                {
                    throw e;
                }
            int ret = SQLcmd.ExecuteNonQuery();
            if (ret == 0)
            {
                throw new Exception("4");
            }
            return true;
        }

        internal static bool deleteMethod<R>(R requested)
        {
            if (isDemo) throw new Exception("5000");
            string requestedName = typeof(R).Name;
            string cmd = "Delete FROM " + requestedName + " where ID='" + typeof(R).GetProperty("ID").GetValue(requested).ToString() +"'";


            SqlCommand SQLcmd = new SqlCommand();
            SQLcmd.CommandText = cmd;
            try
            {
                SQLcmd.Connection = getConnection();
            }
            catch (Exception e)
            {
                throw e;
            }
            int ret=SQLcmd.ExecuteNonQuery();
            if (ret==0)
            {
                throw new Exception("5");
            }
            return true;
        }


        internal static bool deleteJoin<R,R2>(R requested1, R2 requested2, int mode)
        {
            if (isDemo) throw new Exception("5000");
            
            string requestedName1 = typeof(R).Name;
            string requestedName2 = typeof(R2).Name;
            string cmd = "Delete FROM ";
            switch (mode)
            {
                case 1:
                    cmd += requestedName1 + "To" + requestedName2;
                    break;
                case 2:
                    cmd += requestedName2 + "To" + requestedName1;
                    break;
            }
            cmd += " WHERE " + requestedName1 + "ID='" + typeof(R).GetProperty("ID").GetValue(requested1).ToString() + "' AND " + requestedName2 + "ID='" + typeof(R2).GetProperty("ID").GetValue(requested2).ToString() + "'";


            SqlCommand SQLcmd = new SqlCommand();
            SQLcmd.CommandText = cmd;
            try
            {
                SQLcmd.Connection = getConnection();
            }
            catch (Exception e)
            {
                throw e;
            }
            int ret = SQLcmd.ExecuteNonQuery();
          /*  if (ret == 0)
            {
                throw new Exception("6");
            }*/
            return true;
        }



      

        public static ISUser login(string username, string password)
        {
            ISUser tmpUser=new ISUser();
            /*if (username == "admin")
            {
                tmpUser.hasManagementAccess = true;
            }
            else
            {
                tmpUser.hasManagementAccess = false;
            }*/

            try
            {
                List<ISUser> il = selectMethod<ISUser, ISUser>(tmpUser, 5, null, "AND Username='" + username + "' AND Password='" + password + "'");
                return il[0];
            }
            catch (Exception e)
            {
                if (e.Message == "0")
                {
                    throw new Exception("22");
                }
                else
                {
                    throw e;
                }
            }
            
        }

        public static ReadOnlyCollection<T> getDoubleEntries<T>(ReadOnlyCollection<T> bigList, ReadOnlyCollection<T> smallList)
        {
            try
            {
                foreach (T element in bigList)
                {
                    foreach (T element2 in smallList)
                    {
                        if ((String)typeof(T).GetProperty("ID").GetValue(element) == (String)typeof(T).GetProperty("ID").GetValue(element2))
                        {
                            typeof(T).GetProperty("isDouble").SetValue(element, true);
                        }
                    }
                }
            }
            catch (Exception e) { }
            return bigList;
        }




        internal static Image byteArrayToImage(byte[] byteArrayIn)
        {
            try
            {
                MemoryStream ms = new MemoryStream(byteArrayIn);
                Image returnImage = Image.FromStream(ms);
                return returnImage;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal static byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            return ms.ToArray();
        }

        internal static bool saveImage(Image imgObj, ISImage img)
        {
            try
            {
                imgObj.Save(BaseFunctions.storePath + img.ID, ImageFormat.Png);
                imgObj.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal static bool sendMail(string messageBody, string messageSubject, string messageReceiver = "default", bool bodyHtml = true)
        {
            if (messageReceiver == "default")
            {
                messageReceiver = ConfigurationSettings.AppSettings["smtpDefaultReceiver"];
            }
            SmtpClient smtpClient = new SmtpClient();
            NetworkCredential basicCredential = new NetworkCredential(ConfigurationSettings.AppSettings["smtpUser"], ConfigurationSettings.AppSettings["smtpPassword"]);
            MailMessage message = new MailMessage();
            MailAddress fromAddress = new MailAddress(ConfigurationSettings.AppSettings["smtpSender"]);
            smtpClient.EnableSsl = true;
            //smtpClient.Port = 587;
            smtpClient.Host = ConfigurationSettings.AppSettings["smtpServer"];
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = basicCredential;

            message.From = fromAddress;
            message.Subject = messageSubject;
            //Set IsBodyHtml to true means you can send HTML email.
            message.IsBodyHtml = bodyHtml;
            message.Body = messageBody;
            try
            {
                message.To.Add(messageReceiver);
            }
            catch (Exception ex)
            {
                throw new Exception("31");
            }

            try
            {
                smtpClient.Send(message);
            }
            catch (Exception ex)
            {
                //Error, could not send the message
                throw new Exception("32");
            }
            return true;
        }




    }
}
