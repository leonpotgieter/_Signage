using System.ServiceModel;
using System.ServiceModel.Description;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using BusinessObjects;
using System.Configuration;
using EntitySpaces.Interfaces;
using EntitySpaces.Core;
using EntitySpaces.DynamicQuery;
using EntitySpaces;
using System.IO;
//using wodFTPDLib;
using System.Reflection;
using System.ServiceModel.Discovery;
using System.Collections.ObjectModel;
using SmartInspectHelper;
using System.Collections;
using System.Xml.Serialization;

namespace SelfHost
{
    [ServiceContract]
    public class CoreService
    {
        esConnectionElement conn;
        static Collection<CoreHost.Delta> DeltaCollection = new Collection<CoreHost.Delta>();
        
        private void AddUpdateDelta(string name)
        {
            //Deltas = loop, content, schedule, ticker    //LATEST

            DateTime dt = DateTime.Now;
            long ticks = dt.Ticks;
            Boolean found = false;
            foreach (var item in DeltaCollection)
            {
                if (item.name == name)
                {
                    item.lastticks = ticks;
                    Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+"Updating Delta for " + name);
                    found = true;
                }
            }
            if (found == false)
            {
                CoreHost.Delta _newdelta = new CoreHost.Delta();
                _newdelta.lastticks = ticks;
                _newdelta.name = name;
                DeltaCollection.Add(_newdelta);
                Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+"Adding Delta for " + name);
            }
        }

        [OperationContract]
        public long CollectDelta(string name)
        {
            long retval = 1;
            foreach (CoreHost.Delta item in DeltaCollection)
            {
                if (item.name == name)
                {
                    retval = item.lastticks;
                }
            };
            return retval;
        }

        private void InitializeEntitySpacesLoader()
        {
            try
            {
                if (esProviderFactory.Factory == null)
                {
                    esProviderFactory.Factory = new EntitySpaces.Loader.esDataProviderFactory();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void InitDatabaseConnection()
        {
            try
            {
                if (conn == null)
                {
                    conn = new esConnectionElement();
                    conn.Name = "VistaDB4";
                    conn.ConnectionString = @"Data Source=C:\Content\Database\CoreDB.vdb4;Open Mode=NonexclusiveReadWrite;";
                    conn.Provider = "EntitySpaces.VistaDB4Provider";
                    conn.ProviderClass = "DataProvider";
                    conn.SqlAccessType = esSqlAccessType.DynamicSQL;
                    conn.ProviderMetadataKey = "esDefault";
                    //conn.DatabaseVersion = "2005";
                    // --- Assign the Default Connection --- 
                    esConfigSettings.ConnectionInfo.Connections.Add(conn);
                    esConfigSettings.ConnectionInfo.Default = "VistaDB4";
                    
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void InitDB()
        {
            InitDatabaseConnection();
            InitializeEntitySpacesLoader();
        }

        [OperationContract]
        public User VerifyLogin(string login, string passwd)
        {
            InitDB();

            //Purge Schedules older than 7 days from current day5
            try
            {
                DateTime dtNow = DateTime.Now;
                DateTime ts = dtNow.AddDays(-7);

                var sc = new ScheduleCollection();
                sc.Query.Where(sc.Query.Loopend.LessThan(ts));
                sc.LoadAll();
                sc.MarkAllAsDeleted();
                sc.Save();
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+ex.Message);
            }




            //Purge all schedules on finding deleteschedule.txt
            if (File.Exists(@"c:\content\deleteschedule.txt") || File.Exists(@"c:\content\deleteschedule.txt.txt"))
            {
                CoreHost.Properties.Settings.Default.DefaultPort = "null";
                try
                {
                    File.Delete(@"c:\content\deleteschedule.txt");
                }
                catch (Exception ex)
                {
                }
                try
                {
                    File.Delete(@"c:\content\deleteschedule.txt.txt");
                }
                catch (Exception ex)
                {
                }
                Console.WriteLine(DateTime.Now.ToShortTimeString() + "  " + " -- Schedule Purge Requested --");
                ScheduleCollection sc = new ScheduleCollection();
                sc.LoadAll();
                sc.MarkAllAsDeleted();
                sc.Save();
                Console.WriteLine(DateTime.Now.ToShortTimeString() + "  " + " -- Schedules Purged OK --");
            }


            User _verifieduser = new User();
            Boolean Verified = false;
            UserCollection _usr = new UserCollection();
            _usr.LoadAll();
            foreach (User item in _usr)
            {
                if (!Verified) _verifieduser = item;
                if (item.Password == passwd && item.Loginid == login)
                {
                    Verified = true;
                }
            }
            if (Verified == true)
            {
                Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+DateTime.Now.ToString() + "Login OK");
                return _verifieduser;
            }
            else
            {
                Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+DateTime.Now.ToString()+" :Failed Login Attempt");
                try
                {
                    User _newuser = new User();
                    _newuser.Id = 99999;
                    _newuser.Fullname = "failed";
                    _newuser.Loginid = "sys";
                    _newuser.Password = "sys";
                    _newuser.Groupid = "sys";
                    //_newuser.Save();
                    Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+DateTime.Now.ToString()+" :"+_newuser.Fullname + " - Failed Login Attempt");
                    return _newuser; 
                }
                catch (Exception ex)
                {
                   // Console.Write(ex.Message);
                }
            }
            return null;
        }

        [OperationContract]
        public Boolean UpdateScheduleForScreens(ScheduleCollection scheduleCollection)
        {
            InitDB();
            try
            {
                ScheduleCollection currentSchedule = new ScheduleCollection();
                ScheduleCollection newSchedule = new ScheduleCollection();
                //currentSchedule.LoadAll();

                currentSchedule.Query.Where(currentSchedule.Query.Screenname.NotEqual(""));
                currentSchedule.Query.Load();

                if (scheduleCollection.Count > 0) currentSchedule.MarkAllAsDeleted();
                currentSchedule.Save();

                foreach (Schedule schedule in scheduleCollection)
                {
                    Schedule newSch = new Schedule();
                    newSch.Loopname = schedule.Loopname;
                    newSch.Screenname = schedule.Screenname;
                    newSch.Groupname = schedule.Groupname;
                    newSch.Loopstart = schedule.Loopstart;
                    newSch.Loopend = schedule.Loopend;
                    newSchedule.Add(newSch);
                }

                newSchedule.Save();
                SerializeScheduleCollection();
                SerializeLoopCollection();
                SerializeLoopContentCollection();
                SerializeContentCollection();
                SerializeTemplateCollection();
                AddUpdateDelta("schedule");
            }
            catch (Exception ex)
            {
            }

            return true;
        }

        [OperationContract]
        public Boolean UpdateScheduleForGroups(ScheduleCollection scheduleCollection)
        {
            InitDB();
            try
            {
                ScheduleCollection currentSchedule = new ScheduleCollection();
                ScheduleCollection newSchedule = new ScheduleCollection();
                //currentSchedule.LoadAll();

                currentSchedule.Query.Where(currentSchedule.Query.Groupname.NotEqual(""));
                currentSchedule.Query.Load();

                if (scheduleCollection.Count > 0) currentSchedule.MarkAllAsDeleted();
                currentSchedule.Save();

                foreach (Schedule schedule in scheduleCollection)
                {
                    Schedule newSch = new Schedule();
                    newSch.Loopname = schedule.Loopname;
                    newSch.Screenname = schedule.Screenname;
                    newSch.Groupname = schedule.Groupname;
                    newSch.Loopstart = schedule.Loopstart;
                    newSch.Loopend = schedule.Loopend;
                    newSchedule.Add(newSch);
                }

                newSchedule.Save();
                SerializeScheduleCollection();
                SerializeLoopCollection();
                SerializeLoopContentCollection();
                SerializeContentCollection();
                SerializeTemplateCollection();
                AddUpdateDelta("schedule");
            }
            catch (Exception ex)
            {
            }

            return true;
        }

        [OperationContract]
        public ScheduleCollection CollectScheduleForScreen()
        {
            InitDB();
            ScheduleCollection scheduleToReturn = new ScheduleCollection();
            scheduleToReturn.LoadAll();
            ScreenCollection sc = new ScreenCollection();
            sc.LoadAll();
            Console.WriteLine("LICENSING: " + sc.Count().ToString() + " OF 10 LICENSES ACTIVE");
            return scheduleToReturn;
        }

        private void SerializeLoopCollection()
        {
            try
            {
                string FileName = @"c:\content\media\xml\loops.xml";
                LoopCollection loopCollection = new LoopCollection();
                loopCollection.LoadAll();
                Loop[] arrayOfLoop = loopCollection.ToArray();
                using (FileStream fs = new FileStream(FileName, FileMode.Create))
                {
                    XmlSerializer ser = new XmlSerializer(typeof(Loop[]));
                    ser.Serialize(fs, arrayOfLoop);
                    fs.Flush();
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void SerializeTemplateCollection()
        {
            try
            {
                string FileName = @"c:\content\media\xml\template.xml";
                TemplateCollection templateCollection = new TemplateCollection();
                templateCollection.LoadAll();
                Template[] arrayOfTemplate = templateCollection.ToArray();
                using (FileStream fs = new FileStream(FileName, FileMode.Create))
                {
                    XmlSerializer ser = new XmlSerializer(typeof(Template[]));
                    ser.Serialize(fs, arrayOfTemplate);
                    fs.Flush();
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void SerializeContentCollection()
        {
            try
            {
                ObservableCollection<Content> _cntc = new ObservableCollection<Content>();
                ContentCollection c = new ContentCollection();
                c.LoadAll();
                foreach (Content item in c)
                {
                    item.Snapshot = null;
                    _cntc.Add(item);
                }
                
                //ContentCollection contentCollection = new ContentCollection();
                //contentCollection.LoadAll();
                string FileName = @"c:\content\media\xml\content.xml";
                using (FileStream fs = new FileStream(FileName, FileMode.Create))
                {
                    XmlSerializer ser = new XmlSerializer(typeof(ObservableCollection<Content>));
                    ser.Serialize(fs, _cntc);
                    fs.Flush();
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void SerializeLoopContentCollection()
        {
            try
            {
                LoopContentCollection loopcc = new LoopContentCollection();
                loopcc.LoadAll();
                LoopContent[] arrOfLoopContent = loopcc.ToArray();
                string FileName = @"c:\content\media\xml\allloopcontent.xml";
                using (FileStream fs = new FileStream(FileName, FileMode.Create))
                {
                    XmlSerializer ser = new XmlSerializer(typeof(LoopContent[]));
                    ser.Serialize(fs, arrOfLoopContent);
                    fs.Flush();
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void SerializeScheduleCollection()
        {
            string FileName = @"c:\content\media\xml\schedule.xml";
            ScheduleCollection sc = new ScheduleCollection();
            sc.LoadAll();
            Schedule[] arrayOfSchedule = sc.ToArray();
            
            try
            {
                using (FileStream fs = new FileStream(FileName, FileMode.Create))
                {
                    XmlSerializer ser = new XmlSerializer(typeof(Schedule[]));
                    ser.Serialize(fs, arrayOfSchedule);
                    fs.Flush();
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
            }
        }

        //private void SerializeScheduleCollection(string FileName, ServiceReference1.ScheduleCollection schedulec)
        //{
        //    try
        //    {
        //        using (FileStream fs = new FileStream(FileName, FileMode.Create))
        //        {
        //            XmlSerializer ser = new XmlSerializer(typeof(ServiceReference1.ScheduleCollection));
        //            ser.Serialize(fs, schedulec);
        //            fs.Flush();
        //            fs.Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}

        [OperationContract]
        public ScheduleCollection CollectScheduleForScreenGroup(ScreenGroups group, DateTime datetime)
        {
            InitDB();
            ScheduleCollection scheduleCollection = new ScheduleCollection();
            scheduleCollection.LoadAll();
            return scheduleCollection;
        }

        [OperationContract]
        public LoopContentCollection CollectLoopContentForZone(Template zone, Loop loop)
        {
            InitDB();
            LoopContentCollection loopContent = new LoopContentCollection();
            try
            {
                loopContent.Query.Where(loopContent.Query.Zoneid.Equal(zone.Id) && loopContent.Query.Loopid.Equal(loop.Id));
                loopContent.Query.Load();
                foreach (var item in loopContent)
                {
                    Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+"<-- " + item.Medianame);
                }
            }
            catch (Exception ex)
            {
            }
            
            return loopContent;
        }

        [OperationContract]
        public LoopContentCollection CollectLoopContentForZoneByName(string zonename, string loopname)
        {
            InitDB();
            LoopContentCollection loopContent = new LoopContentCollection();
            //tempc.Query.Where(tempc.Query.Name.Equal(zone.Name));
            //tempc.Query.Load();
            try
            {
                loopContent.Query.Where(loopContent.Query.Zonename.Equal(zonename) && loopContent.Query.Loopname.Equal(loopname));
                loopContent.Query.Load();
                foreach (var item in loopContent)
                {
                    Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+"<-- " + item.Medianame);
                }
            }
            catch (Exception ex)
            {
            }

            return loopContent;
        }

        [OperationContract]
        public Boolean ApplyLoopContentCollection(LoopContentCollection contentForZone)
        {
            InitDB();
            try
            {
                LoopContentCollection lcc = new LoopContentCollection();
                lcc.LoadAll();
                foreach (var item in lcc)
                {
                    foreach (var zoneitem in contentForZone)
                    {
                        if (zoneitem.Loopid == item.Loopid && zoneitem.Zoneid == item.Zoneid)
                        {
                            LoopContent lc = new LoopContent();
                            lc.LoadByPrimaryKey((Int32)item.Id);
                            lc.MarkAsDeleted();
                            lc.Save();
                        }
                    }
                }
                foreach (LoopContent item in contentForZone)
                {
                    Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+"--> "+item.Medianame);
                    LoopContent nlc = new LoopContent();
                    nlc.Mediaid = item.Mediaid;
                    nlc.Medianame = item.Medianame;
                    nlc.Loopname = item.Loopname;
                    nlc.Loopid = item.Loopid;
                    nlc.Zoneid = item.Zoneid;
                    nlc.Zonename = item.Zonename;
                    nlc.Templateid = item.Templateid;
                    nlc.Templatename = item.Templatename;
                    nlc.Order = item.Order;
                    nlc.Save();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+ex.Message);
            }
            //SerializeLoopContentCollection();
            AddUpdateDelta("loop");
            return true;
        }

        [OperationContract]
        public Boolean TESTInsertGroup(ScreenGroups s)
        {
            InitDB();
            return true;
        }

        [OperationContract]
        public ScreenGroups CollectSCreenGroup(ScreenGroups s)
        {
            InitDB();
            return null;
        }

        [OperationContract]
        public LoopContentCollection CollectLoopContentCollection()
        {
            InitDB();
            //TemplateCollection tempc = new TemplateCollection();
            ////coll.Query.Where(coll.Query.LastName.Like("Smi%")
            //tempc.Query.Where(tempc.Query.Zonename.Equal("_template"));
            //tempc.Query.Load();
            //return tempc;
            return null;
        }

        [OperationContract]
        public TemplateCollection CollectTemplates()
        {
            InitDB();
            TemplateCollection tempc = new TemplateCollection();
            //coll.Query.Where(coll.Query.LastName.Like("Smi%")
            tempc.Query.Where(tempc.Query.Zonename.Equal("_template"));
            tempc.Query.Load();
            return tempc;
        }

        [OperationContract]
        public TemplateCollection CollectZonesForTemplate(Template zone)
        {
            InitDB();
            TemplateCollection tempc = new TemplateCollection();
            TemplateCollection collectionToReturn = new TemplateCollection();
            try
            {
                tempc.Query.Where(tempc.Query.Name.Equal(zone.Name));
                tempc.Query.Load();
                foreach (var item in tempc)
                {
                    if (item.Zonename != "_template")
                        collectionToReturn.Add(item);
                }
            }
            catch (Exception ex)
            {   
            }
            return collectionToReturn;
        }

        [OperationContract]
        public TemplateCollection CollectZonesForThisTemplateName(String templateName)
        {
            InitDB();
            TemplateCollection tempc = new TemplateCollection();
            TemplateCollection collectionToReturn = new TemplateCollection();
            try
            {
                tempc.Query.Where(tempc.Query.Name.Equal(templateName));
                tempc.Query.Load();
                foreach (var item in tempc)
                {
                    if (item.Zonename != "_template")
                        collectionToReturn.Add(item);
                }
            }
            catch (Exception ex)
            {
            }
            return collectionToReturn;
        }

        [OperationContract]
        public long ChangeMedia(Content mediaName)
        {
            InitDB();

            Template newTemplate = new Template();
            Content newMedia = new Content();
            newMedia.LoadByPrimaryKey((long)mediaName.Id);
            newMedia.Contenttype = mediaName.Contenttype;
            newMedia.Metadata1 = mediaName.Metadata1;
            newMedia.Metadata2 = mediaName.Metadata2;
            newMedia.Metadata3 = mediaName.Metadata3;
            newMedia.Metadata4 = mediaName.Metadata4;
            newMedia.Metadata5 = mediaName.Metadata5;
            newMedia.Metadata6 = mediaName.Metadata6;
            newMedia.Metadata7 = mediaName.Metadata7;
            newMedia.Metadata8 = mediaName.Metadata8;
            newMedia.Metadata9 = mediaName.Metadata9;
            newMedia.Importdate = mediaName.Importdate;
            newMedia.Filesize = mediaName.Filesize;
            newMedia.Filelocation = mediaName.Filelocation;
            newMedia.Description = mediaName.Description;
            newMedia.Name = mediaName.Name;
            newMedia.Snapshot = mediaName.Snapshot;
            newMedia.Uploadstatus = mediaName.Uploadstatus;
            long functionResult = 0;
            try
            {
                newMedia.Save();
                //SerializeContentCollection();
                functionResult = (long)newMedia.Id;
            }
            catch (Exception ex)
            {
                functionResult = 0;
            }
            
            AddUpdateDelta("content");
            return functionResult;
        }

        [OperationContract]
        public Boolean InsertScheduleItem(Schedule schedule)
        {
            InitDB();
            Schedule newSchedule = new Schedule();
            newSchedule.Loopid = schedule.Loopid;
            newSchedule.Loopname = schedule.Loopname;
            newSchedule.Screenid = schedule.Screenid;
            newSchedule.Screenname = schedule.Screenname;
            newSchedule.Groupid = schedule.Groupid;
            newSchedule.Groupname = schedule.Groupname;
            newSchedule.Loopstart = schedule.Loopstart;
            newSchedule.Loopend = schedule.Loopend;
            newSchedule.Createdon = schedule.Createdon;
            Boolean functionResult = false;
            try
            {
                newSchedule.Save();
                functionResult = true;
            }
            catch (Exception ex)
            {
                functionResult = false;
            }
            //SerializeScheduleCollection();
            AddUpdateDelta("schedule");
            return functionResult;
        }

        [OperationContract]
        public long InsertMedia(Content media)
        {
            InitDB();
            long functionResult = 0;

            try
            {
                Content newMedia = new Content();
                newMedia.Contenttype = media.Contenttype;
                newMedia.Filesize = media.Filesize;
                newMedia.Filelocation = media.Filelocation;
                newMedia.Description = media.Description;
                newMedia.Importdate = media.Importdate;
                newMedia.Metadata1 = media.Metadata1;
                newMedia.Metadata2 = media.Metadata2;
                newMedia.Metadata3 = media.Metadata3;
                newMedia.Metadata4 = media.Metadata4;
                newMedia.Metadata5 = media.Metadata5;
                newMedia.Metadata6 = media.Metadata6;
                newMedia.Metadata7 = media.Metadata7;
                newMedia.Metadata8 = media.Metadata8;
                newMedia.Metadata9 = media.Metadata9;
                newMedia.Name = media.Name;
                newMedia.Snapshot = media.Snapshot;
                newMedia.Uploadstatus = -1;

                try
                {
                    newMedia.Save();
                    //SerializeContentCollection();
                    functionResult = (long)newMedia.Id;
                }
                catch (Exception ex)
                {
                    functionResult = 0;
                }
            }
            catch (Exception ex)
            {
                
            }
            return functionResult;
        }

        [OperationContract]
        public Boolean InsertTemplate(Template template)
        {
            InitDB();
            Template newTemplate = new Template();
            newTemplate.Name = template.Name;
            newTemplate.Description = template.Description;
            newTemplate.Created = DateTime.Now;
            newTemplate.Zonename = template.Zonename;
            newTemplate.Zonedescription = template.Zonedescription;
            newTemplate.Opacity = template.Opacity;
            newTemplate.X = template.X;
            newTemplate.Y = template.Y;
            newTemplate.Width = template.Width;
            newTemplate.Height = template.Height;

            if (template.Zonename == "_template")
            {
                Template newZone = new Template();
                newZone.Name = template.Name;
                newZone.X = 0;
                newZone.Y = 0;
                newZone.Width = 1024;
                newZone.Height = 768;
                newZone.Opacity = 1;
                newZone.Zonename = "Background";
                newZone.Zonedescription = "Template Background";
                newZone.Save();
            }
            Boolean functionResult = false;
            try
            {
                newTemplate.Save();
                //SerializeTemplateCollection();
                functionResult = true;
            }
            catch (Exception ex)
            {
                functionResult = false;
            }
            return functionResult;
        }

        [OperationContract]
        public Boolean RemoveTemplate(Template templateName)
        {
            InitDB();
            TemplateCollection tempCollection = new TemplateCollection();
            if (templateName.Zonename == "_template")
            {
                tempCollection.Query.Where(tempCollection.Query.Name.Equal(templateName.Name));
                tempCollection.Query.Load();
                tempCollection.MarkAllAsDeleted();
                tempCollection.Save();
                //SerializeTemplateCollection();
            }
            else
            {
                Template newTemplate = new Template();
                newTemplate.LoadByPrimaryKey((long)templateName.Id);
                newTemplate.MarkAsDeleted();
                newTemplate.Save();
                //SerializeTemplateCollection();
            }
            
            return true;
        }

        [OperationContract]
        public Boolean RemoveCSScreen(CSScreen screen)
        {
            InitDB();
            try
            {
                screen.MarkAsDeleted();
                screen.Save();
                Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+"Display deleted ok");
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+"Error deleting display");
                return false;
            }
            return true;
        }

        [OperationContract]
        public Boolean RemoveCSMedia(CSMedia media)
        {
            InitDB();
            try
            {
                //THIS WILL PURGE THE MEDIA FROM THE CSMEDIA TABLE
                media.MarkAsDeleted();
                media.Save();
                Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+"Media deleted ok");
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+"Error deleting media");
                return false;
            }
            return true;
        }

        [OperationContract]
        public Boolean RemoveCSMediaLoop(CSMedialoop medialoop)
        {
            InitDB();
            try
            {
                //THIS WILL PURGE THE MEDIA FROM THE CSMEDIA TABLE
                medialoop.MarkAsDeleted();
                medialoop.Save();
                Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+"Media Loop deleted ok");
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+"Error deleting media loop");
                return false;
            }
            return true;
        }


        [OperationContract]
        public Boolean RemoveCSDirection(CSDirection direction)
        {
            InitDB();
            try
            {
                direction.MarkAsDeleted();
                direction.Save();
                Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+"Direction deleted ok");
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+"Error deleting direction");
                return false;
            }
            return true;
        }

        [OperationContract]
        public Boolean RemoveCSEvent(CSEvent evnt)
        {
            InitDB();
            try
            {
                CSEvent deleteEvent = new CSEvent();
                deleteEvent.Query.Where(
                deleteEvent.Query.Eventstart.Equal(evnt.Eventstart) && 
                deleteEvent.Query.Eventend.Equal(evnt.Eventend) && 
                deleteEvent.Query.Title.Equal(evnt.Title) && 
                deleteEvent.Query.Screenlocation.Equal(evnt.Screenlocation) && 
                deleteEvent.Query.Description.Equal(evnt.Description)
                );
                if (deleteEvent.Query.Load())
                {
                    deleteEvent.MarkAsDeleted();
                    deleteEvent.Save();
                }
                
                Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+"Event deleted ok");
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+"Error deleting event");
                return false;
            }
            return true;
        }

        [OperationContract]
        public Boolean UpdateScheduleItem(Schedule schedule)
        {
            InitDB();
            Schedule newSchedule = new Schedule();
            newSchedule.LoadByPrimaryKey((long)schedule.Id);
            newSchedule.Loopid = schedule.Loopid;
            newSchedule.Loopname = schedule.Loopname;
            newSchedule.Screenid = schedule.Screenid;
            newSchedule.Screenname = schedule.Screenname;
            newSchedule.Groupid = schedule.Groupid;
            newSchedule.Groupname = schedule.Groupname;
            newSchedule.Loopstart = schedule.Loopstart;
            newSchedule.Loopend = schedule.Loopend;
            newSchedule.Createdon = schedule.Createdon;
            Boolean functionResult = false;
            try
            {
                newSchedule.Save();
                functionResult = true;
            }
            catch (Exception ex)
            {
                functionResult = false;
            }
            //SerializeScheduleCollection();
            AddUpdateDelta("schedule");
            return functionResult;
        }

        [OperationContract]
        public Boolean ChangeTemplate(Template templateName)
        {
            InitDB();
            Template newTemplate = new Template();
            newTemplate.LoadByPrimaryKey((long)templateName.Id);
            newTemplate.Name = templateName.Name;
            newTemplate.Description = templateName.Description;
            newTemplate.Zonename = templateName.Zonename;
            newTemplate.Zonedescription = templateName.Zonedescription;
            newTemplate.X = templateName.X;
            newTemplate.Y = templateName.Y;
            newTemplate.Width = templateName.Width;
            newTemplate.Height = templateName.Height;
            newTemplate.Opacity = templateName.Opacity;
            Boolean functionResult = false;
            try
            {
                newTemplate.Save();
                functionResult = true;
            }
            catch (Exception ex)
            {
                functionResult = false;
            }
            return functionResult;
        }

        [OperationContract]
        public ObservableCollection<Content> CollectMedia()
        {
            InitDB();
            ObservableCollection<Content> _cntc = new ObservableCollection<Content>();
            ContentCollection c = new ContentCollection();
            c.LoadAll();
            foreach (Content item in c)
            {
                _cntc.Add(item);
            }
           
            return _cntc;
        }

        [OperationContract]
        public LoopCollection CollectLoops()
        {
            InitDB();
            LoopCollection lc = new LoopCollection();
            lc.LoadAll();
            return lc;
        }

        [OperationContract]
        public Loop CollectThisLoop(string loopName)
        {
            try
            {
                InitDB();
                Loop lc = new Loop();
                LoopCollection lcollection = new LoopCollection();
                lcollection.Query.Where(lcollection.Query.Name.Equal(loopName));
                lcollection.Query.Load();
                foreach (Loop item in lcollection)
                {
                    Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+"Collecting Loop:" + item.Name);
                    lc = item;
                    break;
                }
                return lc;
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+"COLLECTING LOOP:"+ex.Message);
                return null;
            }
        }

        [OperationContract]
        public Boolean InsertLoop(Loop loop)
        {
            InitDB();
            Loop newLoop = new Loop();
            newLoop.Name = loop.Name;
            newLoop.Description = loop.Description;
            newLoop.Templateid = loop.Templateid;
            newLoop.Templatename = loop.Templatename;
            Boolean functionResult = false;
            try
            {
                newLoop.Save();
                functionResult = true;
            }
            catch (Exception ex)
            {
                functionResult = false;
            }
            return functionResult;
        }

        [OperationContract]
        public Boolean RemoveLoop(Loop loop)
        {
            InitDB();
            try
            {
                Loop deleteLoop = new Loop();
                deleteLoop.LoadByPrimaryKey((long)loop.Id);
                deleteLoop.MarkAsDeleted();
                deleteLoop.Save();
            }
            catch (Exception ex)
            {
                return false;
            }
            AddUpdateDelta("loop");
            return true;
        }



        [OperationContract]
        public ScreenGroupsCollection CollectScreenGroups()
        {
            InitDB();
            ScreenGroupsCollection scrgc = new ScreenGroupsCollection();
            scrgc.LoadAll();
            return scrgc;
        }

        [OperationContract]
        public ScreenCollection CollectScreens()
        {
            InitDB();
            ScreenCollection scrc = new ScreenCollection();
            scrc.LoadAll();
            return scrc;
        }

        [OperationContract]
        public string RegisterDisplayClientLogin(string screenid)
        {
            InitDB();
            string functionResult = "_none"; ;
            ScreenCollection screenCollection = new ScreenCollection();
            screenCollection.Query.Where(screenCollection.Query.Screenname.Equal(screenid));
            screenCollection.Query.Load();
            if (screenCollection.Count <= 0) //New
            {
                Screen newScreen = new Screen();
                newScreen.Screenname = screenid;
                newScreen.Description = "New";
                newScreen.Location = "Default";
                newScreen.Groupid = "Global";
                newScreen.Lastactive = (DateTime)DateTime.Now;
                functionResult = "_registered";
                try
                {
                    newScreen.Save();
                    functionResult = "_registered";
                }
                catch (Exception ex)
                {
                    functionResult = "_error";
                }
            }
            else
            {
                foreach (Screen item in screenCollection)
                {
                    item.Lastactive = (DateTime)DateTime.Now;
                    functionResult = item.Groupid;
                }
                screenCollection.Save();
            }
            return functionResult;
        }

        [OperationContract]
        public Boolean InsertScreen(Screen screen)
        {
            InitDB();
            Screen newScreen = new Screen();
            newScreen.Screenname = screen.Screenname;
            newScreen.Description = screen.Description;
            newScreen.Location = screen.Location;
            newScreen.Groupid = screen.Groupid;
            Boolean functionResult = false;
            try
            {
                newScreen.Save();
                functionResult = true;
            }
            catch (Exception ex)
            {
                functionResult = false;
            }
            return functionResult;
        }

        [OperationContract]
        public UserCollection CollectUsers()
        {
            try
            {
                //Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+"CTCU");
                InitDB();
                Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+"DBINIT_CALL_COLLECT USERS");
                UserCollection scrc = new UserCollection();
                scrc.LoadAll();
                return scrc;
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+ex.Message);
                return null;
            }
        }

        [OperationContract]
        public CSScreenCollection CollectCSScreens()
        {
            try
            {
                Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+"Collect Screens");
                InitDB();
                CSScreenCollection scrc = new CSScreenCollection();
                scrc.LoadAll();
                //Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+"LOAD_ALL");
                return scrc;
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+ex.Message);
                return null;
            }
        }

        [OperationContract]
        public CSMediaCollection CollectCSMedia()
        {
            try
            {
                Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+"Collect CS Media");
                InitDB();
                CSMediaCollection scrc = new CSMediaCollection();
                scrc.LoadAll();
                //Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+"LOAD_ALL");
                return scrc;
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+ex.Message);
                return null;
            }
        }

        [OperationContract]
        public CSMedialoopCollection CollectCSMediaLoops()
        {
            try
            {
                Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+"Collect CS Media Loop");
                InitDB();
                CSMedialoopCollection scrc = new CSMedialoopCollection();
                scrc.LoadAll();
                //Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+"LOAD_ALL");
                return scrc;
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+ex.Message);
                return null;
            }
        }

        [OperationContract]
        public Boolean PurgeCSMediaLoops()
        {
            try
            {
                Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+"Purge CS Media Loop");
                InitDB();
                CSMedialoopCollection scrc = new CSMedialoopCollection();
                scrc.LoadAll();
                scrc.MarkAllAsDeleted();
                scrc.Save();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+ex.Message);
                return false;
            }
        }

        [OperationContract]
        public Boolean InsertUpdateCSMediaLoop(CSMedialoop loop)
        {
            InitDB();
            //Screen newScreen = new Screen();
            CSMedialoop newLoop = new CSMedialoop();

            try
            {
                newLoop.Query.Where(
                newLoop.Query.Loopname.IsNotNull() &&
                newLoop.Query.Mediafilename.IsNotNull() &&
                newLoop.Query.Mediafilename.ToLower().Equal(loop.Mediafilename.ToLower())
                );
            }
            catch (Exception)
            {
            }

            Boolean thereIsNoExistingID = false;
            try
            {
                if (loop.Id > 0)
                {
                    thereIsNoExistingID = false;
                }
                else
                {
                    thereIsNoExistingID = true;
                }
            }
            catch (Exception)
            {
                thereIsNoExistingID = true;
            }

            Boolean functionResult = false;
            try
            {
                if (newLoop.Query.Load() && thereIsNoExistingID == false) //Do an update
                {
                    Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+"Updating MediaLoop Item");
                    newLoop.Order = loop.Order;
                    newLoop.Mediatype = loop.Mediatype;
                    newLoop.Loopname = loop.Loopname;
                    newLoop.Save();
                    functionResult = true;
                    Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+"Updated MediaLoop item OK");
                }
                else                         //Do update
                {
                    Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+"Inserting New MediaLoop Item");
                    newLoop = new CSMedialoop();
                    newLoop.Mediatype = loop.Mediatype;
                    newLoop.Mediafilename = loop.Mediafilename;
                    newLoop.Order = loop.Order;
                    newLoop.Loopname = loop.Loopname;
                    newLoop.Save();
                    functionResult = true;
                    Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+"Inserting New MediaLoop Item OK");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+"Error during MediaLoop Item insert/update:");
                Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+"Message: " + ex.Message);
                functionResult = false;
            }

            return functionResult;
        }

        

        [OperationContract]
        public Boolean InsertUpdateCSDirection(CSDirection direction)
        {
            InitDB();
            //Screen newScreen = new Screen();
            CSDirection newDirection = new CSDirection();

            try
            {
                newDirection.Query.Where(
                    newDirection.Query.Directionalscreenname.IsNotNull() && 
                    newDirection.Query.Meetingroomname.IsNotNull() && 
                    newDirection.Query.Directionalscreenname.ToLower().Equal(direction.Directionalscreenname.ToLower()) &&
                    newDirection.Query.Meetingroomname.ToLower().Equal(direction.Meetingroomname.ToLower()));

            }
            catch (Exception)
            {
        
            }
            
            Boolean functionResult = false;
            try
            {
                if (newDirection.Query.Load()) //Do update
                {
                    Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+"Updating Directional Information");
                    try
                    {
                        newDirection.Directionimageblob = direction.Directionimageblob;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+ex.Message);
                        newDirection.Directionimageblob = null;
                    }
                    
                    newDirection.Directionimagefile = direction.Directionimagefile;
                    newDirection.Directiontext = direction.Directiontext;
                    newDirection.Save();
                    functionResult = true;
                    Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+"Updating Directional Information OK");
                }
                else                         //Do update
                {
                    Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+"Inserting New Directional Information");
                    newDirection = new CSDirection();
                    try
                    {
                        newDirection.Directionimageblob = direction.Directionimageblob;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+ex.Message);
                        newDirection.Directionimageblob = null;
                    }
                    newDirection.Directionimagefile = direction.Directionimagefile;
                    newDirection.Directiontext = direction.Directiontext;
                    newDirection.Meetingroomname = direction.Meetingroomname;
                    newDirection.Directionalscreenname = direction.Directionalscreenname;
                    newDirection.Save();
                    functionResult = true;
                    Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+"Inserting New Directional Information OK");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+"Error during Directional Information insert/update:");
                Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+"Message: "+ex.Message);
                functionResult = false;
            }
            
            return functionResult;
        }

        [OperationContract]
        public long InsertUpdateCSEvent(CSEvent csEvent)
        {
            InitDB();
            CSEvent newEvent = new CSEvent();

            newEvent.Query.Where(newEvent.Query.Id.Equal(csEvent.Id));

            long functionResult = 0;
            try
            {
                if (newEvent.Query.Load()) //Do update
                {
                    Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+"Updating event");
                    newEvent.Screenlocation = csEvent.Screenlocation;
                    newEvent.Customticker = csEvent.Customticker;
                    newEvent.Datetimecreated = csEvent.Datetimecreated;
                    try
                    {
                        newEvent.Eventstart = (DateTime)csEvent.Eventstart;
                        newEvent.Eventend = (DateTime)csEvent.Eventend;
                    }
                    catch (Exception)
                    {
                    }

                    newEvent.Description = csEvent.Description;
                    newEvent.Eventbackground = csEvent.Eventbackground;

                    try
                    {
                        newEvent.Eventlogoblob = csEvent.Eventlogoblob;
                    }
                    catch (Exception)
                    {
                        newEvent.Eventlogoblob = null;
                    }
                    
                    newEvent.Eventlogofile = csEvent.Eventlogofile;
                    
                    newEvent.Template = csEvent.Template;
                    newEvent.Title = csEvent.Title;
                    newEvent.Save();
                    functionResult = (long)newEvent.Id;
                }
                else                         //Do insert
                {
                    Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+"Inserting event");
                    newEvent = new CSEvent();
                    try
                    {
                        newEvent.Screenlocation = csEvent.Screenlocation;
                        newEvent.Customticker = csEvent.Customticker;

                        try
                        {
                            Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+"END:"+csEvent.Eventend.ToString());
                            Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+"START:" + csEvent.Eventstart.ToString());
                            newEvent.Eventend = (DateTime)csEvent.Eventend;
                            newEvent.Eventstart = (DateTime)csEvent.Eventstart;
                        }
                        catch (Exception)
                        {
                        }

                        newEvent.Datetimecreated = csEvent.Datetimecreated;
                        
                        newEvent.Description = csEvent.Description;
                        newEvent.Eventbackground = csEvent.Eventbackground;

                        try
                        {
                            newEvent.Eventlogoblob = csEvent.Eventlogoblob;
                        }
                        catch (Exception)
                        {
                            newEvent.Eventlogoblob = null;
                        }
                        
                        newEvent.Eventlogofile = csEvent.Eventlogofile;
                        
                        newEvent.Template = csEvent.Template;
                        newEvent.Title = csEvent.Title;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+"Err:"+ex.Message);
                    }
                    
                    Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+"Saving Event");
                    newEvent.Save();
                    Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+"Saved Event");
                    functionResult = (long)newEvent.Id;
                }
            }
            catch (Exception)
            {
                functionResult = 0;
            }

            return functionResult;
        }


        [OperationContract]
        public Boolean InsertUpdateCSTemplate(CSTemplate cstemplate)
        {
            InitDB();
     
            CSTemplate newCSTemplate = new CSTemplate();

            newCSTemplate.Query.Where(
                newCSTemplate.Query.Name.IsNotNull() &&
                newCSTemplate.Query.Template.IsNotNull() && 
                newCSTemplate.Query.Name.ToLower().Equal(cstemplate.Name.ToLower()) && 
                newCSTemplate.Query.Template.ToLower().Equal(cstemplate.Template.ToLower())
                );

            Boolean functionResult = false;
            try
            {
                if (newCSTemplate.Query.Load()) //Do update
                {
                    //newCSTemplate.Name = cstemplate.Name;
                    try
                    {
                        Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+"Updating CS Template Attribute");
                        newCSTemplate.Modifiedby = cstemplate.Modifiedby;
                        newCSTemplate.Active = cstemplate.Active;
                        newCSTemplate.Attribvaluenumber = cstemplate.Attribvaluenumber;
                        newCSTemplate.Attribvaluetext = cstemplate.Attribvaluetext;
                        newCSTemplate.Description = cstemplate.Description;
                        newCSTemplate.Lastmodified = DateTime.Now.ToString();
                        newCSTemplate.Save();
                        functionResult = true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+ex.Message);
                    }
                    
                }
                else                         //Do insert
                {
                    try
                    {
                        Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+"Inserting CS Template Attribute");
                        newCSTemplate = new CSTemplate();
                        newCSTemplate.Template = cstemplate.Template;
                        newCSTemplate.Name = cstemplate.Name;
                        newCSTemplate.Modifiedby = cstemplate.Modifiedby;
                        newCSTemplate.Active = cstemplate.Active;
                        newCSTemplate.Attribvaluenumber = cstemplate.Attribvaluenumber;
                        newCSTemplate.Attribvaluetext = cstemplate.Attribvaluetext;
                        newCSTemplate.Description = cstemplate.Description;
                        newCSTemplate.Lastmodified = DateTime.Now.ToString();
                        newCSTemplate.Save();
                        functionResult = true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+ex.Message);
                    }
                }
            }
            catch (Exception)
            {
                functionResult = false;
            }
            return functionResult;
        }

        

        [OperationContract]
        public Boolean InsertUpdateCSMedia(CSMedia _csmedia)
        {
            InitDB();
            //Screen newScreen = new Screen();
            CSMedia newCSMedia = new CSMedia();

            Console.WriteLine("Insert/Update CS Media");

            Console.WriteLine("Query CS Media");
            //newCSMedia.Query.Where(
            //    newCSMedia.Query.Mediafilename.IsNotNull() && 
            //    newCSMedia.Query.Mediafilename.ToLower().Equal(_csmedia.Mediafilename.ToLower()));
            //Console.WriteLine("Query CS Media Done");

            Boolean functionResult = false;
            //try
            //{
            //    if (newCSMedia.Query.Load()) //Do update
            //    {
            //        Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+"Updating CSMedia");
            //        //newCSMedia.Mediablob = csmedia.Mediablob;
            //        newCSMedia.Mediatype = _csmedia.Mediatype;
            //        //newCSMedia.Isfile = 1;
            //        //newCSMedia.Importedbywho = csmedia.Importedbywho;
            //        //newCSMedia.Attributes1 = csmedia.Attributes1;
            //        //newCSMedia.Attributes2 = csmedia.Attributes2;
            //        //newCSMedia.Attributes3 = csmedia.Attributes3;
            //        //newCSMedia.Attributes1 = csmedia.Attributes1;
            //        //newCSMedia.Datetimeimported = (DateTime)DateTime.Now;
            //        newCSMedia.Save();
            //        functionResult = true;
            //    }
            //    else                         //Do update
            //    {
                    try
                    {
                        newCSMedia = new CSMedia();
                        Console.WriteLine(DateTime.Now.ToShortTimeString() + "  " + "Insert New CSMedia");
                        newCSMedia.Mediafilename = _csmedia.Mediafilename;
                        newCSMedia.Mediablob = _csmedia.Mediablob;
                        newCSMedia.Mediatype = _csmedia.Mediatype;
                        newCSMedia.Save();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    
                    functionResult = true;
                //}
            //}
            //catch (Exception ex)
            //{
            //    functionResult = false;
            //    Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+"ERR:"+ex.Message);
            //}
            return functionResult;
        }

        [OperationContract]
        public CSEventCollection CollectCSEvents()
        {
            try
            {
                Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+"Collect Events");
                InitDB();
                CSEventCollection scrc = new CSEventCollection();
                CSEventCollection returnthiscollection = new CSEventCollection();
                CSEventCollection deletethiscollection = new CSEventCollection();
                scrc.LoadAll();
                DateTime dtNow = DateTime.Now.AddDays(-3);
                long dtNowTicks = dtNow.Ticks;
                long dtEventEndTicks = 0;
                DateTime dtEnd;
                Boolean hasDeletedItems = false;
                foreach (CSEvent _event in scrc)
                {
                    dtEnd = (DateTime)_event.Eventend;
                    dtEventEndTicks = dtEnd.Ticks;
                    if (dtNowTicks > dtEventEndTicks) //event has expired
                    {
                        deletethiscollection.Add(_event);
                        hasDeletedItems = true;
                    }
                    else //event is current
                    {
                        returnthiscollection.Add(_event);
                    }
                }
                if (hasDeletedItems)
                {
                    deletethiscollection.MarkAllAsDeleted();
                    deletethiscollection.Save();
                }
                //Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+"LOAD_ALL");
                return returnthiscollection;
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+ex.Message);
                return null;
            }
        }

        [OperationContract]
        public CSTemplateCollection CollectCSTemplates()
        {
            try
            {
                Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+"Collect Templates");
                InitDB();
                CSTemplateCollection scrc = new CSTemplateCollection();
                scrc.LoadAll();
                //Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+"LOAD_ALL");
                return scrc;
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+ex.Message);
                return null;
            }
        }

        [OperationContract]
        public CSMediaCollection CollectCSMediaWithThumbs()
        {
            try
            {
                Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+"Collect Directions");
                InitDB();
                CSMediaCollection scrc = new CSMediaCollection();
                scrc.LoadAll();
                //Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+"LOAD_ALL");
                return scrc;
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+ex.Message);
                return null;
            }
        }

        [OperationContract]
        public CSDirectionCollection CollectCSDirections()
        {
            try
            {
                Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+"Collect Directions");
                InitDB();
                CSDirectionCollection scrc = new CSDirectionCollection();
                scrc.LoadAll();
                //Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+"LOAD_ALL");
                return scrc;
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+ex.Message);
                return null;
            }
        }

        [OperationContract]
        public CSDirectionCollection CollectCSDirectionsForThisDirectionalDisplay(string directionaldisplayid)
        {
            try
            {
                Console.WriteLine(DateTime.Now.ToShortTimeString() + "  " + "Collect Directions");
                InitDB();
                CSDirectionCollection scrc = new CSDirectionCollection();
                CSDirectionCollection tmpscrc = new CSDirectionCollection();
                tmpscrc.LoadAll();
                foreach (CSDirection item in tmpscrc)
                {
                    if (item.Directionalscreenname.ToLower() == directionaldisplayid.ToLower())
                    {
                        scrc.Add(item);
                    }
                }
                //Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+"LOAD_ALL");
                return scrc;
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToShortTimeString() + "  " + ex.Message);
                return null;
            }
        }

        [OperationContract]
        public Boolean RemoveUser(User userName)
        {
            InitDB();
            User deleteUser = new User();
            deleteUser.LoadByPrimaryKey((long)userName.Id);
            deleteUser.MarkAsDeleted();
            deleteUser.Save();
            return true;
        }

        [OperationContract]
        public Boolean RemoveMedia(Content _content)
        {
            InitDB();
            try
            {
                Content deleteContent = new Content();
                deleteContent.LoadByPrimaryKey((long)_content.Id);
                deleteContent.MarkAsDeleted();
                deleteContent.Save();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            //SerializeContentCollection();
            AddUpdateDelta("content");
        }

        [OperationContract]
        public Boolean RemoveTicker(Content _content)
        {
            InitDB();
            Content deleteContent = new Content();
            deleteContent.LoadByPrimaryKey((long)_content.Id);
            deleteContent.MarkAsDeleted();
            deleteContent.Save();
            AddUpdateDelta("ticker");
            return true;
        }

        [OperationContract]
        public Boolean InsertOrUpdateTicker(Content ticker)
        {
            InitDB();
            Boolean functionResult = false;
            Content tickerFound = new Content();
            Boolean updateTicker = false;
            try
            {
            	tickerFound.LoadByPrimaryKey((long)ticker.Id);
                if (tickerFound.Name.Length > 0) updateTicker = true;
            }
            catch (Exception ex)
            {
                updateTicker = false;
            }

            if (updateTicker)
            { //update all ticker data
                tickerFound.Name = ticker.Name;
                tickerFound.Contenttype = ticker.Contenttype;
                tickerFound.Description = ticker.Description;
                tickerFound.Filelocation = "";
                tickerFound.Filesize = 0;
                tickerFound.Metadata1 = ticker.Metadata1;
                tickerFound.Metadata2 = ticker.Metadata2;
                tickerFound.Metadata3 = ticker.Metadata3;
                tickerFound.Metadata4 = ticker.Metadata4;
                tickerFound.Metadata5 = ticker.Metadata5;
                Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+"TICKER SPEED=" + ticker.Metadata5);
                tickerFound.Metadata6 = ticker.Metadata6;
                tickerFound.Metadata7 = ticker.Metadata7;
                tickerFound.Metadata8 = ticker.Metadata8;
                tickerFound.Metadata9 = ticker.Metadata9;
                tickerFound.Snapshot = ticker.Snapshot;
                tickerFound.Importdate = (DateTime)DateTime.Now;
                tickerFound.Save();
                functionResult = true;
            }
            else
            {
                //insert new tickerFound
                Content newTicker = new Content();
                newTicker.Name = ticker.Name;
                newTicker.Description = ticker.Description;
                newTicker.Contenttype = ticker.Contenttype;
                newTicker.Filelocation = "";
                newTicker.Filesize = 0;
                newTicker.Metadata1 = ticker.Metadata1;
                newTicker.Metadata2 = ticker.Metadata2;
                newTicker.Metadata3 = ticker.Metadata3;
                newTicker.Metadata4 = ticker.Metadata4;
                newTicker.Metadata5 = ticker.Metadata5;
                newTicker.Metadata6 = ticker.Metadata6;
                newTicker.Metadata7 = ticker.Metadata7;
                newTicker.Metadata8 = ticker.Metadata8;
                newTicker.Metadata9 = ticker.Metadata9;
                newTicker.Snapshot = ticker.Snapshot;
                newTicker.Importdate = (DateTime)DateTime.Now;
                newTicker.Save();
                functionResult = true;
            }
            AddUpdateDelta("ticker");
            return functionResult;
        }

        [OperationContract]
        public Boolean InsertUser(User user)
        {
            InitDB();
            User newUser = new User();
            newUser.Fullname = user.Fullname;
            newUser.Loginid = user.Loginid;
            newUser.Groupid = user.Groupid;
            newUser.Password = user.Password;
            Boolean functionResult = false;
            try
            {
                newUser.Save();
                functionResult = true;
            }
            catch (Exception ex)
            {
                functionResult = false;
            }
            return functionResult;
        }

        [OperationContract]
        public Boolean InsertCSScreen(CSScreen screen)
        {
            InitDB();
            User newUser = new User();
            CSScreen newScreen = new CSScreen();
            newScreen.Computername = screen.Computername;
            newScreen.Computerlocation = screen.Computerlocation;
            newScreen.Hardwareid = screen.Hardwareid;
            newScreen.Isdirectional = screen.Isdirectional;
            newScreen.Screenlocation = screen.Screenlocation;
            newScreen.Screennumber = screen.Screennumber;
            newScreen.Thumbnail = screen.Thumbnail;
            newScreen.Thumbnaildatetime = screen.Thumbnaildatetime;
            newScreen.Defaulttemplate = screen.Defaulttemplate;
            Boolean functionResult = false;
            try
            {
                newScreen.Save();
                functionResult = true;
            }
            catch (Exception ex)
            {
                functionResult = false;
            }
            return functionResult;
        }

        [OperationContract]
        public Boolean ChangeUser(User user)
        {
            InitDB();
            User newUser = new User();
            newUser.LoadByPrimaryKey((long)user.Id);
            newUser.Fullname = user.Fullname;
            newUser.Loginid = user.Loginid;
            newUser.Groupid = user.Groupid;
            newUser.Password = user.Password;
            Boolean functionResult = false;
            try
            {
                newUser.Save();
                functionResult = true;
            }
            catch (Exception ex)
            {
                functionResult = false;
            }
            return functionResult;
        }

        [OperationContract]
        public Boolean ChangeCSScreen(CSScreen screen)
        {
            InitDB();
            CSScreen newScreen = new CSScreen();
           
            newScreen.LoadByPrimaryKey((long)screen.Id);
            newScreen.Computerlocation = screen.Computerlocation;
            newScreen.Computername = screen.Computername;
            newScreen.Hardwareid = screen.Hardwareid;
            newScreen.Isdirectional = screen.Isdirectional;
            newScreen.Lastqueriedcore = screen.Lastqueriedcore;
            newScreen.Screenlocation = screen.Screenlocation;
            newScreen.Screennumber = screen.Screennumber;
            newScreen.Thumbnail = screen.Thumbnail;
            newScreen.Thumbnaildatetime = screen.Thumbnaildatetime;
   
            Boolean functionResult = false;
            try
            {
                newScreen.Save();
                functionResult = true;
            }
            catch (Exception ex)
            {
                functionResult = false;
            }
            return functionResult;
        }

        [OperationContract]
        public Boolean RemoveGroup(ScreenGroups groupName)
        {
            InitDB();
            ScreenGroups deleteGroup = new ScreenGroups();
            deleteGroup.LoadByPrimaryKey((long)groupName.Id);
            deleteGroup.MarkAsDeleted();
            deleteGroup.Save();
            return true;
        }

        [OperationContract]
        public Boolean ChangeScreenGroup(Screen screen)
        {
            InitDB();
            Screen updateScreen = new Screen();
            updateScreen.LoadByPrimaryKey((long)screen.Id);
            updateScreen.Groupid = screen.Groupid;
            updateScreen.Save();
            return true;
        }

        [OperationContract]
        public Boolean RemoveScreen(Screen screenName)
        {
            InitDB();
            Screen deleteScreen = new Screen();
            deleteScreen.LoadByPrimaryKey((long)screenName.Id);
            deleteScreen.MarkAsDeleted();
            deleteScreen.Save();
            return true;
        }

        [OperationContract]
        public Boolean InsertScreenGroup(ScreenGroups scrg)
        {
            InitDB();
            ScreenGroups l = new ScreenGroups();
            l.Name = scrg.Name;
            l.Description = scrg.Description;
            Boolean functionResult = false;
            try
            {
                l.Save();
                functionResult = true;
            }
            catch (Exception ex)
            {
                functionResult = false;
            }
            return functionResult;
        }

        //[OperationContract]
        //public Boolean InsertTemplate(string name, string description)
        //{
        //    var context = new LightSpeedContext();
        //    context.ConnectionString = @"Data Source = C:\tmp2\SDB.sdf;";
        //    context.DataProvider = DataProvider.SqlServerCE;
        //    context.IdentityMethod = IdentityMethod.KeyTable;
        //    TemplateDescription newTemplate = new TemplateDescription();
        //    try
        //    {
        //        using (var unitOfWork = context.CreateUnitOfWork())
        //        {
        //            newTemplate.name = name;
        //            newTemplate.description = description;
        //            newTemplate.active = true;
        //            unitOfWork.Add(newTemplate);
        //            unitOfWork.SaveChanges();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+ex.Message);
        //    }
            
        //    return true;
        //}

        //[OperationContract]
        //public Boolean InsertSampleDisplay(string l)
        //{
        //    var context = new LightSpeedContext();
        //    context.ConnectionString = @"Data Source = C:\tmp2\SDB.sdf;";
        //    context.DataProvider = DataProvider.SqlServerCE;
        //    context.IdentityMethod = IdentityMethod.KeyTable;
        //    Display newDisplay = new Display();
        //    using (var unitOfWork = context.CreateUnitOfWork())
        //    {
        //        newDisplay.number = 1;
        //        newDisplay.name = "Meeting Room 1";
        //        newDisplay.ip = "127.0.0.1";
        //        newDisplay.active = true;
        //        newDisplay.comment = "Meeting room";
        //        newDisplay.direction = "left.gif";
              
        //        unitOfWork.Add(newDisplay);
        //        unitOfWork.SaveChanges();
        //    }
        //    return true;
        //}

        //[OperationContract]
        //public List<ConferenceDAL.Event_pc> GetAllEvents()
        //{
        //    List<ConferenceDAL.Event_pc> events = new List<Event_pc>();
        //    ConferenceDAL.Event_pc newevent;
        //    var context = new LightSpeedContext();
        //    context.ConnectionString = @"Data Source = C:\tmp2\SDB.sdf;";
        //    context.DataProvider = DataProvider.SqlServerCE;
        //    context.IdentityMethod = IdentityMethod.KeyTable;

        //    try
        //    {
        //        using (var unitOfWork = context.CreateUnitOfWork())
        //        {
        //            //unitOfWork.Find<Event>(Entity.Attribute("PlantType") == "x");
        //            unitOfWork.Find<Event>();
        //            foreach (Event item in unitOfWork)
        //            {
                       
        //                newevent = new Event_pc();
        //                newevent.Comment = item.comment;
        //                newevent.Active = item.active;
        //                newevent.CreatedBy = item.createdby;
        //                newevent.Displayid = Convert.ToInt16(item.displayid);
        //                newevent.Endtime = item.endtime;
        //                newevent.Startttime = item.starttime;
        //                newevent.Templateid = item.templateid;
        //                newevent.Name = item.name;
        //                newevent.DateOnly = item.dateonly;
        //                events.Add(newevent);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return events;
        //    }
        //    return events;
        //}



        //[OperationContract]
        //public List<Template_pc> GetAllTemplates()
        //{
        //    List<ConferenceDAL.Template_pc> templates = new List<Template_pc>();
        //    ConferenceDAL.Template_pc newtemplate;
        //    var context = new LightSpeedContext();
        //    context.ConnectionString = @"Data Source = C:\tmp2\SDB.sdf;";
        //    context.DataProvider = DataProvider.SqlServerCE;
        //    context.IdentityMethod = IdentityMethod.KeyTable;

        //    try
        //    {
        //        using (var unitOfWork = context.CreateUnitOfWork())
        //        {
        //            unitOfWork.Find<TemplateDescription>();
        //            foreach (TemplateDescription item in unitOfWork)
        //            {
        //                newtemplate = new Template_pc();
        //                newtemplate.Active = (Boolean)item.active;
        //                newtemplate.Description = item.description;
        //                newtemplate.Name = item.name;
        //                newtemplate.ID = item.Id;
        //                templates.Add(newtemplate);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return templates;
        //    }
        //    return templates;
        //}


        //[OperationContract]
        //public Boolean InsertResource(ConferenceDAL.Resource_pc _resource)
        //{
        //    var context = new LightSpeedContext();
        //    context.ConnectionString = @"Data Source = C:\tmp2\SDB.sdf;";
        //    context.DataProvider = DataProvider.SqlServerCE;
        //    context.IdentityMethod = IdentityMethod.KeyTable;
        //    Resource newResource = new Resource();

        //    Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+"Storing resource " + _resource.Name);
           
        //    try
        //    {
        //        using (var unitOfWork = context.CreateUnitOfWork())
        //        {
        //            newResource.name = _resource.Name;
        //            newResource.data = _resource.Data;
        //            newResource.datatype = _resource.DataType;
        //            newResource.created = _resource.DateTimeResourceCreated;

        //            unitOfWork.Add(newResource);
        //            unitOfWork.SaveChanges();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+ex.Message);
        //    }
        //    return true;
        //}

        //[OperationContract]
        //public Boolean InsertEvent(ConferenceDAL.Event_pc eventpc)
        //{
        //    var context = new LightSpeedContext();
        //    context.ConnectionString = @"Data Source = C:\tmp2\SDB.sdf;";
        //    context.DataProvider = DataProvider.SqlServerCE;
        //    context.IdentityMethod = IdentityMethod.KeyTable;

        //    Event newEvent = new Event();

        //    try
        //    {
        //        using (var unitOfWork = context.CreateUnitOfWork())
        //        {
        //            newEvent.name = eventpc.Name;
        //            newEvent.comment = eventpc.Comment;
        //            Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+eventpc.Startttime.ToString());
        //            newEvent.starttime = eventpc.Startttime;
        //            newEvent.endtime = eventpc.Endtime;
        //            newEvent.dateonly = eventpc.DateOnly;
        //            newEvent.createdby = "System";
        //            newEvent.templateid = 1;
        //            newEvent.active = eventpc.Active;
        //            newEvent.displayid = (Int16)eventpc.Displayid;
        //            newEvent.displayroomname = eventpc.DisplayRoomName;
        //            newEvent.eventbackground = eventpc.EventBackground;
        //            newEvent.eventdirection = eventpc.EventDirection;
        //            newEvent.eventlogo = eventpc.EventLogo;
        //            newEvent.eventticker = eventpc.EventTicker;
        //            newEvent.notes = eventpc.Notes;

        //            unitOfWork.Add(newEvent);
        //            unitOfWork.SaveChanges();

        //            Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+"Event: "+eventpc.Name + " stored...");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+ex.Message);
        //    }
        //    return true;
        //}

        //[OperationContract]
        //public List<ConferenceDAL.Display_pc> GetAllDisplays()
        //{
        //    List<ConferenceDAL.Display_pc> displaylist = new List<Display_pc>();
        //    ConferenceDAL.Display_pc newdisplay;
        //    var context = new LightSpeedContext();
        //    context.ConnectionString = @"Data Source = C:\tmp2\SDB.sdf;";
        //    context.DataProvider = DataProvider.SqlServerCE;
        //    context.IdentityMethod = IdentityMethod.KeyTable;

        //    try
        //    {
        //        using (var unitOfWork = context.CreateUnitOfWork())
        //        {
        //            //unitOfWork.Find<Event>(Entity.Attribute("PlantType") == "x");
        //            unitOfWork.Find<Display>();
        //            foreach (Display item in unitOfWork)
        //            {
        //                newdisplay = new Display_pc();
        //                newdisplay.ID = item.Id;
        //                newdisplay.Name = item.name;
        //                newdisplay.Comment = item.comment;
        //                newdisplay.Active = item.active;
        //                newdisplay.IP = item.ip;
        //                newdisplay.Direction = item.direction;
        //                newdisplay.Number = item.number;

        //               displaylist.Add(newdisplay);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //return newdisplay;
        //    }
        //    return displaylist;
        //}
        
    }

    class Program
    {
        //public wodFTPD ftpD;
        static void Main(string[] args)
        {
            si.EnableSmartInspect("CS", false);

            DateTime dtnow = DateTime.Now;
            if (dtnow.Month >= 8 || dtnow.Year>2014)
            {
                Console.WriteLine("This Evaluation Software license has unfortunately expired.");
                Console.WriteLine("Please contact your license provider to discuss licensing options.");
                Console.WriteLine("Thank you, iTactix Software");
                Console.ReadLine();
                return;
            }
            
            if (File.Exists(@"c:\content\reset.txt") || File.Exists(@"c:\content\reset.txt.txt"))
            {
                CoreHost.Properties.Settings.Default.DefaultPort = "null";
                try
                {
                    File.Delete(@"c:\content\reset.txt");
                }
                catch (Exception ex)
                {   
                }
                try
                {
                    File.Delete(@"c:\content\reset.txt.txt");
                }
                catch (Exception ex)
                {
                }
                CoreHost.Properties.Settings.Default.Save();
            }

            if (CoreHost.Properties.Settings.Default.DefaultPort == "null")
            {
                Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+"Please specify the service port to use (ie 8888)");
                string p = Console.ReadLine();
                CoreHost.Properties.Settings.Default.DefaultPort = p;
                CoreHost.Properties.Settings.Default.Save();
            }

            Console.Title = "iTactix Server Core";
            string prt = CoreHost.Properties.Settings.Default.DefaultPort;
            Uri baseAddress = new Uri("http://localhost:"+prt+"/iTactixCoreService");
            //Uri baseAddress = new Uri("http://localhost:8888/taggTIXSCoreService");
            // Get the version of the executing assembly (that is, this assembly).
            Assembly assem = Assembly.GetEntryAssembly();
            AssemblyName assemName = assem.GetName();
            Version ver = assemName.Version;

            //CALCULATE THE DATE OF THE BUILD
            DateTime dt = new DateTime(2000, 1, 1, 00, 00, 00);
            dt = dt.AddDays(ver.Build);
            //System.Windows.MessageBox.Show(dt.ToLongDateString());

            //ANOTHER (SHORTER) WAY OF COLLECTING THE DATA
            string versionString = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            //System.Windows.MessageBox.Show(versionString);

            versionString = ver.ToString() + " " + dt.ToShortDateString();
            Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+"Core Services Version: " + versionString);
            
            //SoftwareVersion = versionString;

            if (!Directory.Exists(@"c:\content"))
            {
                Directory.CreateDirectory(@"c:\content");
            }
            if (!Directory.Exists(@"c:\content\Database"))
            {
                Directory.CreateDirectory(@"c:\content\Database");
            }
            if (!Directory.Exists(@"c:\content\Media\Xml"))
            {
                Directory.CreateDirectory(@"c:\content\Media\Xml");
            }
            if (!Directory.Exists(@"c:\content\Media"))
            {
                Directory.CreateDirectory(@"c:\content\Media");
            }
            if (!Directory.Exists(@"c:\content\Media\Video"))
            {
                Directory.CreateDirectory(@"c:\content\Media\Video");
            }
            if (!Directory.Exists(@"c:\content\Media\Flash"))
            {
                Directory.CreateDirectory(@"c:\content\Media\Flash");
            }
            if (!Directory.Exists(@"c:\content\Media\Audio"))
            {
                Directory.CreateDirectory(@"c:\content\Media\Audio");
            }
            if (!Directory.Exists(@"c:\content\Media\Images"))
            {
                Directory.CreateDirectory(@"c:\content\Media\Images");
            }
            if (!Directory.Exists(@"c:\content\Media\Ticker"))
            {
                Directory.CreateDirectory(@"c:\content\Media\Ticker");
            }
            
            if (!Directory.Exists(@"c:\content\Conference"))
            {
                Directory.CreateDirectory(@"c:\content\Conference");
            }
            if (!Directory.Exists(@"c:\content\Conference\Logos"))
            {
                Directory.CreateDirectory(@"c:\content\Conference\Logos");
            }
            if (!Directory.Exists(@"c:\content\Conference\Directional"))
            {
                Directory.CreateDirectory(@"c:\content\Conference\Directional");
            }
            if (!Directory.Exists(@"c:\content\Conference\Video"))
            {
                Directory.CreateDirectory(@"c:\content\Conference\Video");
            }
            if (!Directory.Exists(@"c:\content\Conference\Images"))
            {
                Directory.CreateDirectory(@"c:\content\Conference\Images");
            }
            if (!Directory.Exists(@"c:\content\Conference\Tickers"))
            {
                Directory.CreateDirectory(@"c:\content\Conference\Tickers");
            }

            if (!File.Exists(@"c:\content\Database\CoreDB.vdb4"))
            {
                string appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                string dbPath = appPath + @"\Database\CoreDB.vdb4";
                File.Copy(dbPath, @"c:\content\Database\CoreDB.vdb4");
                dbPath = appPath + @"\Database\CoreDB.vdc4";
                File.Copy(dbPath, @"c:\content\Database\CoreDB.vdc4");
                Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+"Database initialized and ready...");
                //Directory.CreateDirectory(@"c:\content\Media\Video");
            }
            if (!File.Exists(@"c:\content\Media\Ticker\Ticker.txt"))
            {
                string appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                string tickerPath = appPath + @"\Ticker.txt";
                File.Copy(tickerPath, @"c:\content\Media\Ticker\Ticker.txt");
                tickerPath = appPath + @"\TickerMeta.txt";
                File.Copy(tickerPath, @"c:\content\Media\Ticker\Tickermeta.txt");
                Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+"Ticker diagnostic initialized...");
            }


            BasicHttpBinding httpBinding = new BasicHttpBinding();
            Int32 SERVICE_BUFFER_SIZE = 6000000;
            httpBinding.MaxReceivedMessageSize = SERVICE_BUFFER_SIZE;
            httpBinding.MaxBufferSize = SERVICE_BUFFER_SIZE;
            httpBinding.ReaderQuotas.MaxArrayLength = SERVICE_BUFFER_SIZE;
            httpBinding.ReaderQuotas.MaxBytesPerRead = SERVICE_BUFFER_SIZE;

            System.ServiceModel.Channels.Binding bnding = httpBinding;

            
            // Create the ServiceHost.
            using (ServiceHost host = new ServiceHost(typeof(CoreService), baseAddress))
            {
                
               // // Add ServiceDiscoveryBehavior
               //host.Description.Behaviors.Add(new ServiceDiscoveryBehavior());
               //ServiceEndpoint sep = new ServiceEndpoint();
               //sep.Behaviors.Add(new BasicHttpBinding());

                //ConcurrencyMode cm = new Co
               
                
               
               
                //host.AddServiceEndpoint(sep);
            
               // // Add a UdpDiscoveryEndpoint
               //// Uri l = new Uri("127.0.0.1");
               // host.AddServiceEndpoint(new UdpDiscoveryEndpoint(DiscoveryVersion.WSDiscovery11));

                // Enable metadata publishing.
                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                
                smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;
                host.Description.Behaviors.Add(smb);

                //NB!!!!!! Concurrency is set here to Multiple from Single (which is the default)
                host.Description.Behaviors.Find<ServiceBehaviorAttribute>().ConcurrencyMode = ConcurrencyMode.Multiple;

                //ServiceDebugBehavior sdb = new ServiceDebugBehavior();
              
               
                // Open the ServiceHost to start listening for messages. Since
                // no endpoints are explicitly configured, the runtime will create
                // one endpoint per base address for each service contract implemented
                // by the service.
                host.Open();
               

                //FTP SERVER

                //wodFTPD ftpD = new wodFTPD();
                //ftpD.Authentication = AuthenticationsEnum.authPassword;

                ////ftpD.Start(21);
                ////ftpD.GreetingMessage = "iTactix Core Services. Welcome...";
                //try
                //{
                //    ftpD.Start();
                //}
                //catch (Exception ex)
                //{
                //    try
                //    {
                //        System.Threading.Thread.Sleep(500);
                //        ftpD.Start();
                //    }
                //    catch (Exception em)
                //    {
                //        try
                //        {
                //            System.Threading.Thread.Sleep(1000);
                //            ftpD.Start();
                //        }
                //        catch (Exception ey)
                //        {
                //            try
                //            {
                //                System.Threading.Thread.Sleep(2000);
                //                ftpD.Start();
                //            }
                //            catch (Exception ez)
                //            {
                //            }
                //        }
                //    }
                //}

                //ftpD.Progress += new _IwodFTPDEvents_ProgressEventHandler(ftpD_Progress);
                //ftpD.TransferComplete += new _IwodFTPDEvents_TransferCompleteEventHandler(ftpD_TransferComplete);

                //ftpD.LoginPassword += delegate(FtpUser User, string Login, string Password, ref FtpActions Action)
                //{
                //    //Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+"FTP Login");
                //    if (Password == "bugaboo")
                //    {
                //        Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+"File Transfer Session allowed");
                //        User.HomeDir = @"c:\content";
                //        Action = FtpActions.Allow;
                //    }
                //    else
                //    {
                //        Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+"File Transfer Session denied");
                //        Action = FtpActions.Deny;
                //    }
                //};

                Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+"The service is ready at {0}", baseAddress);
                Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+"Press <Enter> to stop the service.");
                Console.ReadLine();

                // Close the ServiceHost.
                host.Close();
            }
        }

        //static void ftpD_TransferComplete(FtpUser User, bool Successful)
        //{
        //    Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+"File transfer status: "+Successful.ToString());
        //}

        //static void ftpD_Progress(FtpUser User, int Position, int Total)
        //{
        //    Console.WriteLine(DateTime.Now.ToShortTimeString()+"  "+Position.ToString()+" bytes transferred");
        //}

    }
}
