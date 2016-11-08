using System;
using System.Windows.Forms;

namespace AppCore.DebugGUI
{
    public partial class EventViewer : Form
    {
        public EventViewer()
        {
            InitializeComponent();
        }

        private void EventViewer_Load(object sender, EventArgs e)
        {
            var eventHandler = new AppEvent.appEvent(AppSettings.AppSettings.appEventsPath);
            foreach(AppEvent.appEvent.appEventEntry entry in eventHandler.getEvents())
            {
                string[] newRow = new string[] { entry.timeStamp, entry.error };
                dataGridView1.Rows.Add(newRow);
            }
        }
    }
}
