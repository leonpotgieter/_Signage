using Raven.Client.Document;
using Raven.Client.Indexes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SmartInspectHelper;
using Raven.Json.Linq;
using System.Windows;
using System.IO;

namespace DAL
{
    class RDB
    {
        private DocumentStore documentStore;
        static Collection<DAL.Delta> DeltaCollection = new Collection<DAL.Delta>();

        public RDB()
        {
            InitDB();
        }

        public Boolean InitDB()
        {
            try
            {
                string url = "http://" + DisplayClient.Properties.Settings.Default.ServiceAddress + ":" + DisplayClient.Properties.Settings.Default.Port;
                //documentStore = new DocumentStore { Url="http://127.0.0.1:8999" };
                documentStore = new DocumentStore { Url = url };
                documentStore.DefaultDatabase = "signage";
                documentStore.Initialize();
                return true;
            }
            catch (Exception)
            {
                si.sii("RDB FAIL");
                return false;
            }
        }

        private void AddUpdateDelta(string name)
        {
            //Deltas = loop, content, schedule, ticker    //LATEST

            DateTime dt = DateTime.Now;
            long ticks = dt.Ticks;
            Boolean found = false;

            using (var session = documentStore.OpenSession())
            {
                var d = (from x in session.Query<Delta>()
                          where x.name == name
                          select x).SingleOrDefault();

                if (d == null) //doesn't exist, add it
                {
                    Delta nd = new Delta();
                    nd.lastticks = ticks;
                    nd.name = name;
                    session.Store(nd);
                    session.SaveChanges();
                } else //update it
                {
                    d.lastticks = ticks;
                    session.Store(d);
                    session.SaveChanges();
                }
            }
        }

        public Loop CollectThisLoop(string loopName)
        {
                InitDB();
                Loop lc = new Loop();

                using (var session = documentStore.OpenSession())
                {
                    var d = (from x in session.Query<Loop>()
                             where x.Name == loopName
                             select x).SingleOrDefault();
                    lc = d;
                }

                try
                {
                    Console.WriteLine(DateTime.Now.ToShortTimeString() + "  " + "Collecting Loop:" + lc.Name);
                    return lc;
                }
                catch (Exception)
                {
                    return null;
                }
        }

        public ObservableCollection<Template> CollectZonesForThisTemplateName(String templateName)
        {
            InitDB();
            ObservableCollection<Template> tempc = new ObservableCollection<Template>();
            ObservableCollection<Template> collectionToReturn = new ObservableCollection<Template>();
            try
            {
                using (var session = documentStore.OpenSession())
                {
                    var z = from x in session.Query<Template>()
                             where x.Name == templateName && 
                             x.Zonename != "_template"
                             select x;
                    return new ObservableCollection<Template>(z);
                }
                //tempc.Query.Where(tempc.Query.Name.Equal(templateName));
                //tempc.Query.Load();
                //foreach (var item in tempc)
                //{
                //    if (item.Zonename != "_template")
                //        collectionToReturn.Add(item);
                //}
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public long CollectDelta(string name)
        {
            InitDB();
            using (var session = documentStore.OpenSession())
            {
                var dc = (from x in session.Query<Delta>()
                         where x.name == name
                         select x).SingleOrDefault();
                try
                {
                    return dc.lastticks;
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }

        public Boolean AddTestUser()
        {
            InitDB();
            User newUser = new User();
            using (var session = documentStore.OpenSession())
            {
                User u = new User();
                u.Fullname = "Administrator";
                u.Enabled = true;
                u.Loginid = "admin";
                u.Password = "password";
                u.Groupid = "Administrators";
                session.Store(u);
                session.SaveChanges();
            }
            return true;
        }

        public Boolean ApplyLoopContentCollection(ObservableCollection<DAL.LoopContent> contentForZone)
        {
            InitDB();
            try
            {
                //LoopContentCollection lcc = new LoopContentCollection();
                ObservableCollection<DAL.LoopContent> lcc = new ObservableCollection<LoopContent>();
                //lcc.LoadAll();
                using (var session = documentStore.OpenSession())
                {
                    var _lcc = from i in session.Query<DAL.LoopContent>()
                               select i;
                    //First delete all
                    foreach (var item in _lcc)
                    {
                        foreach (var zoneitem in contentForZone)
                        {
                            if (zoneitem.Loopid == item.Loopid && zoneitem.Zoneid == item.Zoneid)
                            {
                                session.Delete(item);
                                session.SaveChanges();
                                //LoopContent lc = new LoopContent();
                                //lc.LoadByPrimaryKey((Int32)item.Id);
                                //lc.MarkAsDeleted();
                                //lc.Save();
                            }
                        }    
                    }
                    //Then resave
                    foreach (LoopContent item in contentForZone)
                    {
                        //Console.WriteLine(DateTime.Now.ToShortTimeString() + "  " + "--> " + item.Medianame);
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
                        session.Store(nlc);
                        session.SaveChanges();
                    }
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToShortTimeString() + "  " + ex.Message);
            }
            //SerializeLoopContentCollection();
            //AddUpdateDelta("loop");
            return true;
        }

        public string ChangeMedia(Content mediaName)
        {
            InitDB();

            Template newTemplate = new Template();
            Content newMedia = new Content();
            string functionResult = "0";
            using (var session = documentStore.OpenSession())
            {
                newMedia = session.Load<Content>(mediaName.Id);
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
                session.Store(newMedia);
                functionResult = newMedia.Id;
            }
            //newMedia.LoadByPrimaryKey((long)mediaName.Id);
            //AddUpdateDelta("content");
            return functionResult;
        }

        public ObservableCollection<LoopContent> CollectLoopContentForZoneByName(string zonename, string loopname)
        {
            InitDB();
            ObservableCollection<LoopContent> loopContent = new ObservableCollection<LoopContent>();
            try
            {
                using (var session = documentStore.OpenSession())
                {
                    var lc = from x in session.Query<LoopContent>()
                             where x.Zonename == zonename && x.Loopname == loopname
                             select x;
                    loopContent = new ObservableCollection<LoopContent>(lc);
                }
                foreach (var item in loopContent)
                {
                    Console.WriteLine(DateTime.Now.ToShortTimeString() + "  " + "<-- " + item.Medianame);
                }
            }
            catch (Exception ex)
            {
            }
            return loopContent;
        }

        public Boolean ChangeScreenGroup(Screen screen)
        {
            InitDB();
            Screen updateScreen = new Screen();
            using (var session = documentStore.OpenSession())
            {
                updateScreen = session.Load<Screen>(screen.Id);
                updateScreen.Groupid = screen.Groupid;
                session.Store(updateScreen);
                session.SaveChanges();
            }
            //updateScreen.LoadByPrimaryKey((long)screen.Id);
            //updateScreen.Groupid = screen.Groupid;
            //updateScreen.Save();
            return true;
        }

        public Boolean ChangeTemplate(Template templateName)
        {
            InitDB();
            Template newTemplate = new Template();
            using (var session = documentStore.OpenSession())
            {
                newTemplate = session.Load<Template>(templateName.Id);
                newTemplate.Name = templateName.Name;
                newTemplate.Description = templateName.Description;
                newTemplate.Zonename = templateName.Zonename;
                newTemplate.Zonedescription = templateName.Zonedescription;
                newTemplate.X = templateName.X;
                newTemplate.Y = templateName.Y;
                newTemplate.Width = templateName.Width;
                newTemplate.Height = templateName.Height;
                newTemplate.Opacity = templateName.Opacity;
                session.Store(newTemplate);
                session.SaveChanges();
                return true;
            }
            return false;
        }

        public Boolean ChangeUser(User user)
        {
            InitDB();
            User newUser = new User();
            using (var session = documentStore.OpenSession())
            {
                newUser = session.Load<User>(user.Id);
                //newUser.LoadByPrimaryKey((long)user.Id);
                newUser.Fullname = user.Fullname;
                newUser.Loginid = user.Loginid;
                newUser.Groupid = user.Groupid;
                newUser.Password = user.Password;
                session.Store(newUser);
                session.SaveChanges();
                return true;
            }
            return false;
        }

        public ObservableCollection<LoopContent> CollectLoopContentForZone(Template zone, Loop loop)
        {
            InitDB();
            using (var session = documentStore.OpenSession())
            {
                var lc = from x in session.Query<LoopContent>()
                         where x.Zoneid == zone.Id && x.Loopid == loop.Id
                         select x;
                return new ObservableCollection<LoopContent>(lc);
            }
            return null;
        }

        public ObservableCollection<Loop> CollectLoops()
        {
            InitDB();
            using (var session = documentStore.OpenSession())
            {
                var lc = from x in session.Query<Loop>()
                         select x;
                return new ObservableCollection<Loop>(lc);
            }
            return null;
        }

        public ObservableCollection<Content> CollectMedia()
        {
            InitDB();
            using (var session = documentStore.OpenSession())
            {
                var lc = from x in session.Query<Content>()
                         select x;
                return new ObservableCollection<Content>(lc);
            }
            return null;

            //ObservableCollection<Content> _cntc = new ObservableCollection<Content>();
            //ContentCollection c = new ContentCollection();
            //c.LoadAll();
            //foreach (Content item in c)
            //{
            //    _cntc.Add(item);
            //}

            //return _cntc;
        }

        public ObservableCollection<Schedule> CollectScheduleForScreen()
        {
            InitDB();
            using (var session = documentStore.OpenSession())
            {
                var lc = from x in session.Query<Schedule>()
                         select x;
                return new ObservableCollection<Schedule>(lc);
            }
            return null;


            //ScheduleCollection scheduleToReturn = new ScheduleCollection();
            //scheduleToReturn.LoadAll();
            //ScreenCollection sc = new ScreenCollection();
            //sc.LoadAll();
            //Console.WriteLine("LICENSING: " + sc.Count().ToString() + " OF 10 LICENSES ACTIVE");
            //return scheduleToReturn;
        }

        public ObservableCollection<Screengroups> CollectScreenGroups()
        {
            InitDB();
            using (var session = documentStore.OpenSession())
            {
                var lc = from x in session.Query<Screengroups>()
                         select x;
                return new ObservableCollection<Screengroups>(lc);
            }
            return null;
        }

        public ObservableCollection<Screen> CollectScreens()
        {
            InitDB();
            using (var session = documentStore.OpenSession())
            {
                var lc = from x in session.Query<Screen>()
                         select x;
                return new ObservableCollection<Screen>(lc);
            }
            return null;
        }

        public ObservableCollection<Template> CollectTemplates()
        {
            InitDB();
            using (var session = documentStore.OpenSession())
            {
                var lc = from x in session.Query<Template>()
                         where x.Zonename == "_template"
                         select x;
                return new ObservableCollection<Template>(lc);
            }
            return null;
        }

        public ObservableCollection<User> CollectUsers()
        {
            InitDB();
            using (var session = documentStore.OpenSession())
            {
                var lc = from x in session.Query<User>()
                         select x;
                return new ObservableCollection<User>(lc);
            }
            return null;
        }

        public ObservableCollection<Template> CollectZonesForTemplate(Template zone)
        {
            InitDB();
            try
            {
                using (var session = documentStore.OpenSession())
                {
                    var lc = from x in session.Query<Template>()
                             where x.Name == zone.Name && 
                             x.Zonename != "_template"
                             select x;
                    return new ObservableCollection<Template>(lc);
                }
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        public Boolean InsertLoop(Loop loop)
        {
            InitDB();
            using (var session = documentStore.OpenSession())
            {
                Loop newLoop = new Loop();
                newLoop.Name = loop.Name;
                newLoop.Description = loop.Description;
                newLoop.Templateid = loop.Templateid;
                newLoop.Templatename = loop.Templatename;
                session.Store(newLoop);
                session.SaveChanges();
                return true;
            }
            return false;
        }

        public string InsertMedia(Content media)
        {
            InitDB();
            string functionResult = "0";

            using (var session = documentStore.OpenSession())
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
                session.Store(newMedia);
                session.SaveChanges();
                return newMedia.Id;
            }
            return "0";
        }

        public Boolean InsertScheduleItem(Schedule schedule)
        {
            InitDB();
            using (var session = documentStore.OpenSession())
            {
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
                session.Store(newSchedule);
                session.SaveChanges();
                return true;
            }
            return false;
        }

        public Boolean InsertScreen(Screen screen)
        {
            InitDB();
            using (var session = documentStore.OpenSession())
            {
                Screen newScreen = new Screen();
                newScreen.Screenname = screen.Screenname;
                newScreen.Description = screen.Description;
                newScreen.Location = screen.Location;
                newScreen.Groupid = screen.Groupid;
                session.Store(newScreen);
                session.SaveChanges();
                return true;
            }
            return false;
        }

        public Boolean InsertScreenGroup(Screengroups scrg)
        {
            InitDB();
            using (var session = documentStore.OpenSession())
            {
                Screengroups l = new Screengroups();
                l.Name = scrg.Name;
                l.Description = scrg.Description;
                session.Store(l);
                session.SaveChanges();
                return true;
            }
            return false;
        }

        public Boolean InsertTemplate(Template template)
        {
            InitDB();
            using (var session = documentStore.OpenSession())
            {
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
                    session.Store(newZone);
                }
                session.Store(newTemplate);
                session.SaveChanges();
                return true;
            }
            return false;
        }

        public Boolean InsertUser(User user)
        {
            InitDB();
            using (var session = documentStore.OpenSession())
            {
                User newUser = new User();
                newUser.Fullname = user.Fullname;
                newUser.Loginid = user.Loginid;
                newUser.Groupid = user.Groupid;
                newUser.Password = user.Password;
                session.Store(newUser);
                session.SaveChanges();
                return true;
            }
            return false;
        }

        public Boolean RemoveGroup(Screengroups groupName)
        {
            InitDB();
            using (var session = documentStore.OpenSession())
            {
                Screengroups deleteGroup = new Screengroups();
                deleteGroup = session.Load<Screengroups>(groupName.Id);
                session.Delete(deleteGroup);
                session.SaveChanges();
                return true;
            }
            return false;
        }

        public string RegisterDisplayClientLogin(string screenid)
        {
            InitDB();

            si.sii("Registering Displayclient with DB: " + screenid);
            string functionResult = "_none";

            using (var session = documentStore.OpenSession())
            {
                var screenC = from x in session.Query<Screen>()
                              //where x.Screenname == screenid
                              select x;

                Boolean f = false;
                foreach (var s in screenC)
                {
                    if (s.Screenname == screenid)
                    {
                        f = true; //do update
                        s.Lastactive = (DateTime)DateTime.Now;
                        functionResult = s.Groupid;
                        session.Store(s);
                        session.SaveChanges();
                        return functionResult;
                    }
                }

                if (!f) //not found, add screen
                {
                    Screen newScreen = new Screen();
                    newScreen.Screenname = screenid;
                    newScreen.Description = "New";
                    newScreen.Location = "Default";
                    newScreen.Groupid = "Global";
                    newScreen.Lastactive = (DateTime)DateTime.Now;
                    functionResult = "_registered";
                    session.Store(newScreen);
                    session.SaveChanges();
                    return functionResult;
                }
            }

            return null;
        }

        public Boolean RemoveLoop(Loop loop)
        {
            InitDB();
            using (var session = documentStore.OpenSession())
            {
                Loop deleteLoop = new Loop();
                deleteLoop = session.Load<Loop>(loop.Id);
                session.Delete(deleteLoop);
                session.SaveChanges();
                return true;
            }
            return false;
        }

        public Boolean RemoveMedia(Content _content)
        {
            InitDB();
            using (var session = documentStore.OpenSession())
            {
                Content deleteContent = new Content();
                deleteContent = session.Load<Content>(_content.Id);
                session.Delete(deleteContent);
                session.SaveChanges();
                return true;
            }
            return false;
        }

        public Boolean RemoveScreen(Screen screenName)
        {
            InitDB();
            using (var session = documentStore.OpenSession())
            {
                Screen deleteScreen = new Screen();
                deleteScreen = session.Load<Screen>(screenName.Id);
                session.Delete(deleteScreen);
                session.SaveChanges();
                return true;
            }
            return false;
        }

        public Boolean RemoveTemplate(Template templateName)
        {
            InitDB();
            using (var session = documentStore.OpenSession())
            {
                if (templateName.Zonename == "_template")
                {
                    var tc = from x in session.Query<Template>()
                             where x.Name == templateName.Name
                             select x;
                    session.Delete(tc);
                }
                else
                {
                    Template newTemplate = new Template();
                    newTemplate = session.Load<Template>(templateName.Id);
                    session.Delete(newTemplate);
                }
                session.SaveChanges();
                return true;
            }
            return false;
        }

        public Boolean RemoveTicker(Content _content)
        {
            InitDB();
            using (var session = documentStore.OpenSession())
            {
                Content deleteContent = new Content();
                deleteContent = session.Load<Content>(_content.Id);
                session.Delete(deleteContent);
                session.SaveChanges();
                return true;
            }
            return false;
        }

        public Boolean RemoveUser(User userName)
        {
            InitDB();
            using (var session = documentStore.OpenSession())
            {
                User deleteUser = new User();
                deleteUser = session.Load<User>(userName.Id);
                session.Delete(deleteUser);
                session.SaveChanges();
                return true;
            }
            return false;
        }

        public Boolean UpdateScheduleForScreens(ObservableCollection<Schedule> scheduleCollection)
        {
            InitDB();

            using (var session = documentStore.OpenSession())
            {
                var currentSchedule = from x in session.Query<Schedule>()
                                      where x.Screenname != ""
                                      select x;

                ObservableCollection<Schedule> newSchedule = new ObservableCollection<Schedule>();
                //ScheduleCollection newSchedule = new ScheduleCollection();

                if (scheduleCollection.Count > 0)
                {
                    session.Delete(currentSchedule);
                    session.SaveChanges();
                }

                foreach (Schedule schedule in scheduleCollection)
                {
                    Schedule newSch = new Schedule();
                    newSch.Loopname = schedule.Loopname;
                    newSch.Screenname = schedule.Screenname;
                    newSch.Groupname = schedule.Groupname;
                    newSch.Loopstart = schedule.Loopstart;
                    newSch.Loopend = schedule.Loopend;
                    session.Store(newSch);
                }
                session.SaveChanges();
            }


            //SerializeScheduleCollection();
            //SerializeLoopCollection();
            //SerializeLoopContentCollection();
            //SerializeContentCollection();
            //SerializeTemplateCollection();

            return true;
        }

        public User VerifyLogin(string login, string passwd)
        {
            InitDB();
            User _verifieduser = new User();
            Boolean Verified = false;

            using (var session = documentStore.OpenSession())
            {
                var _usr = from x in session.Query<User>()
                           select x;
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
                    Console.WriteLine(DateTime.Now.ToShortTimeString() + "  " + DateTime.Now.ToString() + "Login OK");
                    return _verifieduser;
                } else 
                {
                    User _newuser = new User();
                    _newuser.Id = "99999";
                    _newuser.Fullname = "failed";
                    _newuser.Loginid = "sys";
                    _newuser.Password = "sys";
                    _newuser.Groupid = "sys";
                    return _newuser;
                }
            }
            return null;
        }

        public Boolean UpdateScheduleForGroups(ObservableCollection<Schedule> scheduleCollection)
        {
            InitDB();
            using (var session = documentStore.OpenSession())
            {
                var currentSchedule = from x in session.Query<Schedule>()
                                      where x.Groupname != ""
                                      select x;

                //ScheduleCollection newSchedule = new ScheduleCollection();

                if (scheduleCollection.Count > 0)
                {
                    session.Delete(currentSchedule);
                    session.SaveChanges();
                }

                foreach (Schedule schedule in scheduleCollection)
                {
                    Schedule newSch = new Schedule();
                    newSch.Loopname = schedule.Loopname;
                    newSch.Screenname = schedule.Screenname;
                    newSch.Groupname = schedule.Groupname;
                    newSch.Loopstart = schedule.Loopstart;
                    newSch.Loopend = schedule.Loopend;
                    session.Store(newSch);
                }
                session.SaveChanges();
            }
            //SerializeScheduleCollection();
            //SerializeLoopCollection();
            //SerializeLoopContentCollection();
            //SerializeContentCollection();
            //SerializeTemplateCollection();
            return true;
        }

        public Boolean InsertOrUpdateTicker(Content ticker)
        {
            InitDB();
            return true;
        }

        public Boolean StoreAttachment(string filename, string format, string attachmentid)
        {
            InitDB();
            try
            {
                using (var session = documentStore.OpenSession())
                {
                    var dbCommands = session.Advanced.DocumentStore.DatabaseCommands;
                    byte[] f = File.ReadAllBytes(@filename);
                    Stream stream = new MemoryStream(f);
                    var optionalMetaData = new RavenJObject();
                    optionalMetaData["Format"] = format;
                    dbCommands.PutAttachment(attachmentid, Guid.NewGuid(),
                        stream, optionalMetaData);
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
            return true;
        }

        public Boolean GetAndSaveAttachment(string filename, string attachmentID)
        {
            
            using (var session = documentStore.OpenSession())
            {
                var dbCommands = session.Advanced.DocumentStore.DatabaseCommands;
                var attachment = dbCommands.GetAttachment(attachmentID);

                //byte[] theBytes = attachment.Data();

                //byte[] rawBytes = attachment.Data().ReadData(); 

                //byte[] m_Bytes = ReadToEnd(theStream);
                
                //File.WriteAllBytes(@filename, theBytes);
                var fileStream = File.Create(@filename);
                attachment.Data().Seek(0, SeekOrigin.Begin);
                attachment.Data().CopyTo(fileStream);
                //myOtherObject.InputStream.Seek(0, SeekOrigin.Begin);
                //myOtherObject.InputStream.CopyTo(fileStream);
                fileStream.Close();

                
                //byte[] att = attachment.Data;
                //Stream stream = new MemoryStream(attachment.Data);
                //var fileFormat = attachment.Metadata["Format"];
                //File.WriteAllBytes(@filename, theFile);
                //using (Stream file = File.Create(@filename))
                //{
                //    byte[] buffer = new byte[8 * 1024];
                //    int len;
                //    while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
                //    {
                //        output.Write(buffer, 0, len);
                //    }
                //    CopyStream(theStream, file);
                //}
            }
            return true;
        }

        public static byte[] ReadToEnd(System.IO.Stream stream)
        {
            long originalPosition = 0;

            if (stream.CanSeek)
            {
                originalPosition = stream.Position;
                stream.Position = 0;
            }

            try
            {
                byte[] readBuffer = new byte[4096];

                int totalBytesRead = 0;
                int bytesRead;

                while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
                {
                    totalBytesRead += bytesRead;

                    if (totalBytesRead == readBuffer.Length)
                    {
                        int nextByte = stream.ReadByte();
                        if (nextByte != -1)
                        {
                            byte[] temp = new byte[readBuffer.Length * 2];
                            Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                            Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                            readBuffer = temp;
                            totalBytesRead++;
                        }
                    }
                }

                byte[] buffer = readBuffer;
                if (readBuffer.Length != totalBytesRead)
                {
                    buffer = new byte[totalBytesRead];
                    Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
                }
                return buffer;
            }
            finally
            {
                if (stream.CanSeek)
                {
                    stream.Position = originalPosition;
                }
            }
        }

        public static void CopyStream(System.IO.Stream input, System.IO.Stream output)
        {
            byte[] buffer = new byte[8 * 1024];
            int len;
            while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, len);
            }
        }

        //public Boolean SaveOrganization(RDBOrganization o)
        //{
        //    try
        //    {
        //        using (var session = documentStore.OpenSession())
        //        {
        //            session.Store(o);
        //            session.SaveChanges();
        //            return true;
        //            MessageBox.Show("ORGID = " + o.Id);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return false;
        //    }
        //}

        //public string SaveThread(RDBThread p)
        //{
        //    try
        //    {
        //        using (var session = documentStore.OpenSession())
        //        {
        //            session.Store(p);
        //            session.SaveChanges();
        //            return p.Id;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return "!";
        //    }
        //}

        //public void CheckTheCloudForAllNew(string userid)
        //{
        //    CheckForUpdatedChannelSubscriptionsForThisUser(userid);
        //}

        //public Boolean SaveChannel(Channel c)
        //{
        //    try
        //    {
        //        using (var session = documentStore.OpenSession())
        //        {
        //            session.Store(c);
        //            session.SaveChanges();
        //            return true;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return false;
        //    }
        //}

        ////public Boolean SaveUpdatedChannelSubscriptionsForThisUser(ObservableCollection<ChannelSub> c, string userid)
        ////{
        ////    using (var session = documentStore.OpenSession())
        ////    {
        ////        //Delete previous choices
        ////        var _subs = from i in session.Query<ChannelSub>()
        ////                       where i.UID == userid
        ////                       select i;
        ////        foreach (var item in _subs)
        ////        {
        ////            session.Delete(item);
        ////        }
                
        ////        //Save Selections
        ////        foreach (var sub in c)
        ////        {
        ////            session.Store(sub);
        ////        }
        ////        session.SaveChanges();
        ////    }

        ////    var _lastUpdated = new LastUpdated();
        ////    _lastUpdated = new Xml().LoadLastUpdatedFromXml();
        ////    _lastUpdated.ChannelSubs = DateTime.Now;          

        ////    new Xml().SaveLastUpdatedToXml(_lastUpdated);
        ////    new Xml().SaveChannelSubscriptionsToXml(c);

        ////    return true;
        ////}

        //public Boolean SaveSurvey(Survey s)
        //{
        //    try
        //    {
        //        using (var session = documentStore.OpenSession())
        //        {
        //            session.Store(s);
        //            session.SaveChanges();
        //            return true;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return false;
        //    }
        //}

        //public Boolean SaveFocusGroup(Group group)
        //{
        //    try
        //    {
        //        using (var session = documentStore.OpenSession())
        //        {
        //            session.Store(group);
        //            session.SaveChanges();
        //            return true;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return false;
        //    }
        //}

        //public Boolean SaveGroupPost(GroupPost post)
        //{
        //    try
        //    {
        //        using (var session = documentStore.OpenSession())
        //        {
        //            session.Store(post);
        //            session.SaveChanges();
        //            return true;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return false;
        //    }
        //}

        //public ObservableCollection<Group> GetUpdatedGroups()
        //{
        //    var lu = new LastUpdated();
        //    lu = new Xml().LoadLastUpdatedFromXml();
        //    DateTime dt = GetUTC(lu.Groups);

        //    if (Helpers.General.IsUpdateRequired(dt, _GroupUpdateInterval) == false)
        //    {
        //        //Update isn't required
        //        return new ObservableCollection<Group>();
        //    }

        //    try
        //    {
        //        using (var session = documentStore.OpenSession())
        //        {
        //            var results = session.Advanced.LuceneQuery<Group>()
        //                    .WhereGreaterThan("@metadata.Last-Modified", dt)
        //                    .ToArray();

        //            si.sii("CHECKING FOR UPDATED GROUPS (" + dt.ToString() + ") - FOUND:" + results.Count().ToString());

        //            if (results.Any())
        //            {
        //                lu.Groups = DateTime.Now;
        //                new Xml().SaveLastUpdatedToXml(lu);
        //                return new ObservableCollection<Group>(results);
        //            }
        //            else
        //            {
        //                lu.Groups = DateTime.Now;
        //                new Xml().SaveLastUpdatedToXml(lu);
        //                return new ObservableCollection<Group>();
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return new ObservableCollection<Group>();
        //    }
        //}

        //public Boolean CheckForUpdatedChannelSubscriptionsForThisUser(string userid)
        //{
        //    //LastUpdated lu = new LastUpdated();
        //    //lu = new Xml().LoadLastUpdatedFromXml();
        //    //DateTime dt = GetUTC(lu.ChannelSubs);

        //    //try
        //    //{
        //    //    using (var session = documentStore.OpenSession())
        //    //    {
        //    //        var results = session.Advanced.LuceneQuery<ChannelSub>()
        //    //                .WhereEquals("UID", userid).AndAlso()
        //    //                .WhereGreaterThan("@metadata.Last-Modified", dt)
        //    //                .ToArray();

        //    //        si.sii("CHECKING FOR NEWER CHANNEL SUBS (" + dt.ToString() + ") FOR " + userid + " - FOUND:" + results.Count().ToString());
        //    //        ObservableCollection<ChannelSub> _subscriptions = new ObservableCollection<ChannelSub>();

        //    //        if (results.Count() > 0)
        //    //        {
        //    //            //Save the latest channel list
        //    //            ObservableCollection<Channel> c = new ObservableCollection<Channel>();
        //    //            c = GetLatestChannelList();

        //    //            //next get subscriptions and save subscription list
        //    //            var _subs = from i in session.Query<ChannelSub>()
        //    //                        where i.UID == userid
        //    //                        select i;
        //    //            foreach (var item in _subs)
        //    //            {
        //    //                _subscriptions.Add(item);
        //    //            }

        //    //            lu.ChannelSubs = DateTime.Now;
        //    //            new Xml().SaveChannelSubscriptionsToXml(_subscriptions);
        //    //            return true;
        //    //        }
        //    //        else
        //    //        {
        //    //            return false;
        //    //        }
        //    //    }
        //    //}
        //    //catch (Exception)
        //    //{
        //    //    return false;
        //    //}
        //    return false;
        //}

        //public ObservableCollection<RDBItem> FetchAndStoreUpdatedNews()
        //{
        //    //Get time of last update from SQLite and Convert it to UTC which is what the database stores its time in
        //    DateTime dt1 = new DateTime(2014, 04, 11, 14, 04, 35); //for testing
        //    DateTime dt = dt1.ToUniversalTime();

        //    try
        //    {
        //        //determine what to fetch and fetch it
        //        List<string> parms = new List<string> { "Gauteng", "Western Cape" };
        //        using (var session = documentStore.OpenSession())
        //        {
        //            var results = session.Advanced.LuceneQuery<RDBItem>()
        //                    .WhereGreaterThan("@metadata.Last-Modified", dt)
        //                    .AndAlso()
        //                    .WhereEquals("Section","P")   
        //                    .AndAlso()
        //                    .WhereIn("Province", parms)  
        //                    .ToArray();

        //            si.sii("CHECKING FOR NEWS (" + dt1.ToString() + ") - FOUND:" + results.Count().ToString());

        //            foreach (var item in new ObservableCollection<RDBItem>(results))
        //            {
        //                RavenJObject metadata = session.Advanced.GetMetadataFor(item); 
        //                DateTime d = metadata.Value<DateTime>("Last-Modified");
        //                si.sii(item.Id+" - "+d.ToLocalTime().ToString());
        //            }

        //            if (results.Any())
        //            {
        //                return new ObservableCollection<RDBItem>(results);
        //            }
        //            else
        //            {
        //                return new ObservableCollection<RDBItem>();
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return new ObservableCollection<RDBItem>();
        //    }
        //}

        //public ObservableCollection<Survey> GetUpdatedSurveysFromDB()
        //{
        //    var lu = new LastUpdated();
        //    lu = new Xml().LoadLastUpdatedFromXml();
        //    DateTime dt = GetUTC(lu.Survey);
        //    try
        //    {
        //        using (var session = documentStore.OpenSession())
        //        {
        //            var results = session.Advanced.LuceneQuery<Survey>()
        //                    .WhereGreaterThan("@metadata.Last-Modified", dt)
        //                    .ToArray();

        //            si.sii("CHECKING FOR SURVEYS (" + dt.ToString() + ") - FOUND:" + results.Count().ToString());
        //            var surveys = new ObservableCollection<Survey>();

        //            if (results.Any())
        //            {
        //                return new ObservableCollection<Survey>(results);
        //            }
        //            else
        //            {
        //                return surveys;
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return new ObservableCollection<Survey>();
        //    }
        //}

        //public ObservableCollection<ChannelPost> GetUpdatedChannelPosts()
        //{
        //    var lu = new LastUpdated();
        //    lu = new Xml().LoadLastUpdatedFromXml();
        //    DateTime dt = GetUTC(lu.ChannelPosts);
        //    try
        //    {
        //        using (var session = documentStore.OpenSession())
        //        {
        //            var results = session.Advanced.LuceneQuery<ChannelPost>()
        //                    .WhereEquals("IsParent", true).AndAlso()
        //                    .WhereGreaterThan("@metadata.Last-Modified", dt)
        //                    .ToArray();

        //            si.sii("CHECKING FOR ChannelPosts (" + dt.ToString() + ") - FOUND:" + results.Count().ToString());
        //            var channelposts = new ObservableCollection<ChannelPost>();

        //            if (results.Any())
        //            {
        //                return new ObservableCollection<ChannelPost>(results);
        //            }
        //            else
        //            {
        //                return channelposts;
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return new ObservableCollection<ChannelPost>();
        //    }
        //}

        //public ObservableCollection<GroupPost> GetUpdatedGroupPosts()
        //{
        //    var lu = new LastUpdated();
        //    lu = new Xml().LoadLastUpdatedFromXml();
        //    DateTime dt = GetUTC(lu.GroupPosts);
        //    try
        //    {
        //        using (var session = documentStore.OpenSession())
        //        {
        //            var results = session.Advanced.LuceneQuery<GroupPost>()
        //                    //.WhereEquals("IsParent", true).AndAlso()
        //                    .WhereGreaterThan("@metadata.Last-Modified", dt)
        //                    .ToArray();

        //            si.sii("CHECKING FOR New Group Posts (" + dt.ToString() + ") - FOUND:" + results.Count().ToString());
        //            var groupposts = new ObservableCollection<GroupPost>();

        //            if (results.Any())
        //            {
        //                //lu.GroupPosts = DateTime.Now;
        //                //new Xml().SaveLastUpdatedToXml(lu);
        //                return new ObservableCollection<GroupPost>(results);
        //            }
        //            else
        //            {
        //                //lu.GroupPosts = DateTime.Now;
        //                //new Xml().SaveLastUpdatedToXml(lu);
        //                return groupposts;
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return new ObservableCollection<GroupPost>();
        //    }
        //}

        //public ObservableCollection<ChannelComment> GetUpdatedChannelCommentsForThisPost(string parentID)
        //{
        //    var lu = new LastUpdated();
        //    lu = new Xml().LoadLastUpdatedFromXml();
        //    DateTime dt = GetUTC(lu.ChannelComments);
        //    try
        //    {
        //        using (var session = documentStore.OpenSession())
        //        {
        //            var results = session.Advanced.LuceneQuery<ChannelComment>()
        //                .WhereEquals("ParentID", parentID).AndAlso()
        //                .WhereGreaterThan("@metadata.Last-Modified", dt)
        //                .ToArray();

        //            si.sii("CHECKING FOR ChannelComments for ID (" + parentID.ToString() + ") - FOUND:" + results.Count().ToString());
        //            var channelcomments = new ObservableCollection<ChannelComment>();

        //            if (results.Any())
        //            {
        //                return new ObservableCollection<ChannelComment>(results);
        //            }
        //            else
        //            {
        //                return channelcomments;
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return new ObservableCollection<ChannelComment>();
        //    }
        //}

        //public ObservableCollection<GroupPostComment> GetUpdatedGroupPostCommentsForThisPost(string parentID)
        //{
        //    var lu = new LastUpdated();
        //    lu = new Xml().LoadLastUpdatedFromXml();
        //    DateTime dt = GetUTC(lu.GroupPostComments);
        //    try
        //    {
        //        using (var session = documentStore.OpenSession())
        //        {
        //            var results = session.Advanced.LuceneQuery<GroupPostComment>()
        //                .WhereEquals("ParentID", parentID).AndAlso()
        //                .WhereGreaterThan("@metadata.Last-Modified", dt)
        //                .ToArray();

        //            si.sii("CHECKING FOR Group Post Comments for ID (" + parentID.ToString() + ") - FOUND:" + results.Count().ToString());
        //            var groupcomments = new ObservableCollection<GroupPostComment>();

        //            if (results.Any())
        //            {
        //                return new ObservableCollection<GroupPostComment>(results);
        //            }
        //            else
        //            {
        //                return groupcomments;
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return new ObservableCollection<GroupPostComment>();
        //    }
        //}

        ////public async Task<ObservableCollection<NewsArticle>> GetUpdatedNewsAsync()
        ////{
        ////    var lu = new LastUpdated();
        ////    lu = new Xml().LoadLastUpdatedFromXml();
        ////    DateTime dt = GetUTC(lu.News);
        ////    var news = new ObservableCollection<NewsArticle>();

        ////    try
        ////    {
        ////        using (var session = documentStore.OpenAsyncSession())
        ////        {
        ////            //var token = await session.LoadAsync<Token>(string.Format("UserName/{0}", userName));
        ////            var results = await session.Advanced.AsyncLuceneQuery<NewsArticle>()
        ////                .WhereGreaterThan("@metadata.Last-Modified", dt)
        ////                .ToListAsync();
                    
        ////            if (results.Any())
        ////            {
        ////                return new ObservableCollection<NewsArticle>(results);
        ////            }
        ////            else
        ////            {
        ////                return news;
        ////            }
        ////        }
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        return news;
        ////    }
        ////}

        ////public async Task<ObservableCollection<NewsArticle>> UpdateNewsAsync()
        ////{
        ////    var _updatednews = new ObservableCollection<NewsArticle>();
        ////    _updatednews = await GetUpdatedNewsAsync();

        ////    var _xmlnews = new ObservableCollection<NewsArticle>();
        ////    _xmlnews = NewsArticle.LoadNewsCollectionFromXml("");
            
        ////    Boolean newArticlesFound = false;

        ////    foreach (var n in _updatednews)
        ////    {
        ////        var found = false;
        ////        foreach (var x in _xmlnews)
        ////        {
        ////            if (x.Id == n.Id) found = true;
        ////        }
        ////        if (!found)
        ////        {
        ////            _xmlnews.Add(n);
        ////            newArticlesFound = true;
        ////        }
        ////    }

        ////    var lu = new LastUpdated();
        ////    lu = new Xml().LoadLastUpdatedFromXml();
        ////    lu.News = DateTime.Now;
        ////    new Xml().SaveLastUpdatedToXml(lu);

        ////    if (newArticlesFound) NewsArticle.SaveNewsCollectionToXml(_xmlnews);

        ////    return _xmlnews;
        ////}

        //public ObservableCollection<Channel> GetLatestChannelList()
        //{
        //    using (var session = documentStore.OpenSession())
        //    {
        //        var results = session.Advanced.LuceneQuery<Channel>()
        //                        .ToList();
        //        new Xml().SaveChannelsToXml(new ObservableCollection<Channel>(results));
        //        return new ObservableCollection<Channel>(results);
        //    }
        //}

        //public string TestMeta()
        //{
        //    string s = "";
        //    try
        //    {
        //        using (var session = documentStore.OpenSession())
        //        {
        //            //var date = new DateTime(from.Year, from.Month, from.Day, from.Hour, from.Minute, from.Second, DateTimeKind.Utc)
                 
        //            var results = session.Advanced.LuceneQuery<RDBItem>()
        //                .WhereGreaterThan("@metadata.Last-Modified", "20120411")
        //                .ToArray();

        //            return results.Count().ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.ToString();
        //    }
        //}

        //public Boolean StoreItems(ObservableCollection<RDBItem> news)
        //{
        //    using (var session = documentStore.OpenSession())
        //    {
        //        foreach (var item in news)
        //        {
        //            session.Store(item);    
        //        }
        //        session.SaveChanges();
        //    }
        //    return true;
        //}

        //public Boolean StoreChannels(ObservableCollection<RDBChannel> channels)
        //{
        //    using (var session = documentStore.OpenSession())
        //    {
        //        foreach (var item in channels)
        //        {
        //            session.Store(item);
        //        }
        //        session.SaveChanges();
        //    }
        //    return true;
        //}

        //public Boolean StoreChannelPost(ChannelPost _post)
        //{
        //    try
        //    {
        //        using (var session = documentStore.OpenSession())
        //        {
        //            session.Store(_post);
        //            session.SaveChanges();
        //            return true;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}

        //public Boolean StoreGroupPost(GroupPost _post)
        //{
        //    try
        //    {
        //        using (var session = documentStore.OpenSession())
        //        {
        //            session.Store(_post);
        //            session.SaveChanges();
        //            return true;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}

        //public Boolean StoreChannelComment(ChannelComment _comment)
        //{
        //    try
        //    {
        //        using (var session = documentStore.OpenSession())
        //        {
        //            session.Store(_comment);
        //            session.SaveChanges();
        //            return true;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}

        //public Boolean StoreGroupPostComment(GroupPostComment _comment)
        //{
        //    try
        //    {
        //        using (var session = documentStore.OpenSession())
        //        {
        //            session.Store(_comment);
        //            session.SaveChanges();
        //            return true;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}

        //public Boolean StoreSurveyCompleted(SurveyCompleted surveycompleted)
        //{
        //    try
        //    {
        //        using (var session = documentStore.OpenSession())
        //        {
        //            session.Store(surveycompleted);
        //            session.SaveChanges();
        //            return true;
        //        }
        //    }
        //    catch (Exception x)
        //    {
        //        return false;
        //    }
        //}

        //private DateTime GetUTC(DateTime dt)
        //{
        //    DateTime d = new DateTime();
        //    d = dt.ToUniversalTime();
        //    return d;
        //}

    }
}
