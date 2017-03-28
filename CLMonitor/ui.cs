using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CLMonitor
{
    public partial class ui : Form
    {
        public class Listing
        {
            public DateTime QueryDate;
            public Uri Uri;
            public string Contact;
            public IEnumerable<Uri> Images;
            public DateTime PostDate;
            public DateTime UpdateDate;
            public string Title;
            public string Category;
            public decimal Price;
            public string Location;
            public string Description;
        }

        public List<Listing> Listings;

        public Dictionary<string, List<Listing>> GroupListings = new Dictionary<string, List<Listing>>();

        public void LoadGroups()
        {
            //GroupListings = Listings.GroupBy(l => l.Uri).Where(l => Program.urls.Contains(l.Key.ToString())).OrderByDescending(l => l.OrderByDescending(b => b.UpdateDate).Last().UpdateDate).ToDictionary(l => (l.Last().Title.Contains("[NO TITLE!]") ? "X-" : "") + l.Key.Segments.Last() + " - " + l.First().Title, l => l.ToList());
            GroupListings = Listings.GroupBy(l => l.Uri).OrderByDescending(l => l.OrderByDescending(b => b.UpdateDate).Last().UpdateDate).ToDictionary(l => (l.Last().Title.Contains("[NO TITLE!]") ? "X-" : "") + l.Key.Segments.Last() + " - " + l.First().Title, l => l.OrderByDescending(b => b.UpdateDate).ToList());
            if (Listings != null)
                //lstURIs.DataSource = Listings.GroupBy(l => (Program.urls.Contains(l.Uri.ToString()) ? "" : "X-") +  l.Uri.Segments.Last() + " - " + l.Title).Select(l => l.Key).ToList();
                //lstURIs.DataSource = Listings.GroupBy(l => l.Uri).Where(l => Program.urls.Contains(l.Key.ToString())).Select(l => (l.Last().Title.Contains("[NO TITLE!]") ? "" : "X-") + l.Key.Segments.Last() + " - " + l.First().Title).ToList();
                lstURIs.DataSource = GroupListings.Select(l => l.Key).ToList();
        }

        public ui()
        {
            InitializeComponent();
        }

        private void ui_Load(object sender, EventArgs e)
        {

        }



        private List<Listing> CurrentListing;

        private void btnRight_Click(object sender, EventArgs e)
        {
            if(int.Parse(btnRight.Tag.ToString()) < CurrentListing.Count() - 1)
                btnRight.Tag = (int.Parse(btnRight.Tag.ToString()) + 1).ToString();
            DisplayListing(CurrentListing.ElementAt(int.Parse(btnRight.Tag.ToString())));
        }

        public void DisplayListing(Listing listing)
        {
            lblQueryDate.Text = listing.QueryDate.ToString("G");
            linkURL.Text = listing.Uri.ToString();
            linkURL.Links.Clear();
            var lnk = new LinkLabel.Link();
            lnk.LinkData = listing.Uri.ToString();
            lnk.Description = listing.Title;
            lnk.Start = 0;
            lnk.Enabled = true;
            lnk.Length = linkURL.Text.Length;
            linkURL.Links.Add(lnk);
            lblDetails.Text = string.Format("Contact: {0}\nCategory: {1}\nLocation: {2}\nPrice: {3}\nDescription: {4}", listing.Contact, listing.Category, listing.Location, listing.Price.ToString("c"), listing.Description.Replace("\\n","\n"));
        }

        public void ClearDisplay()
        {
            lblQueryDate.Text = "";
            linkURL.Text = "";
            lblDetails.Text = "";
        }

        public void EnableDisableButtons()
        {
            btnLeft.Enabled = btnRight.Enabled = (CurrentListing != null);
            //if (CurrentListing == null)
            //{
            //    btnLeft.Enabled = false;
            //    btnRight.Enabled = false;
            //}
            //else
            //{
            //    btnLeft.Enabled = true;
            //    btnRight.Enabled = true;
            //}
        }

        private void lstURIs_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentListing = GroupListings[lstURIs.SelectedItem.ToString()];  //Listings.GroupBy(l => l.Uri.Segments.Last() + " - " + l.Title).FirstOrDefault(l => l.Key == lstURIs.SelectedItem.ToString());
            EnableDisableButtons();
            if (CurrentListing != null)
            {
                DisplayListing(CurrentListing.Last());
                btnRight.Tag = CurrentListing.Count - 1;
                txtTag.Text = Program.urls.ContainsKey(CurrentListing.First().Uri.ToString()) ? Program.urls[CurrentListing.First().Uri.ToString()]:"";
            }
            else
            {
                ClearDisplay();
                
            }
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            if(int.Parse(btnRight.Tag.ToString()) > 0)
                btnRight.Tag = (int.Parse(btnRight.Tag.ToString()) - 1).ToString();
            DisplayListing(CurrentListing.ElementAt(int.Parse(btnRight.Tag.ToString())));
        }

        private void btnQueryAll_Click(object sender, EventArgs e)
        {
            Program.QueryAll();
            ReadFromFile(Program.urlFile + "_Data.CSV");
            LoadGroups();
        }

        private void btnQueryOne_Click(object sender, EventArgs e)
        {
            Program.QuerySingle(new Uri(linkURL.Text));
            ReadFromFile(Program.urlFile + "_Data.CSV");
            LoadGroups();
        }

        public void ReadFromFile(string filename)
        {
            if(System.IO.File.Exists(filename))
                Listings = System.IO.File.ReadAllLines(filename)
                    .Select(r => r.Split('|'))
                    .Select(c => new ui.Listing()
                    {
                        QueryDate = DateTime.Parse(c[0]),
                        Uri = new Uri(c[1]),
                        Title = c[2],
                        PostDate = DateTime.Parse(c[3]),
                        UpdateDate = DateTime.Parse(c[4]),
                        Category = c[5],
                        Price = decimal.Parse(c[6], System.Globalization.NumberStyles.Any),
                        Location = c[7],
                        Description = c[8],
                        Images = c[9].Split(',').Select(i => new Uri(i))
                    }
                    ).ToList();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            if (!Program.urls.ContainsKey(txtNewURL.Text.Trim()))
            {
                System.IO.File.AppendAllLines(Program.urlFile, new string[] { txtNewURL.Text.Trim() + "," + txtNewTag.Text.Trim() });
                Program.ReadURLs(Program.urlFile);
                Program.QuerySingle(new Uri(txtNewURL.Text.Trim()));
                ReadFromFile(Program.urlFile + "_Data.CSV");
                LoadGroups();
            }
            else
            {
                MessageBox.Show("Listing already monitored!");
            }
                txtNewURL.Text = "";
                txtNewTag.Text = "";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            System.IO.File.WriteAllLines(Program.urlFile, Program.urls.Where(u => u.Key != linkURL.Text).Select(r => r.Key + "," + r.Value));
            Program.ReadURLs(Program.urlFile);
            ReadFromFile(Program.urlFile + "_Data.CSV");
            LoadGroups();
        }

        private void linkURL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Link.LinkData as string);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Program.urls[linkURL.Text] = txtTag.Text.Trim().Replace(",", "");
            System.IO.File.WriteAllLines(Program.urlFile, Program.urls.Select(r => r.Key + "," + r.Value));
            Program.ReadURLs(Program.urlFile);
            ReadFromFile(Program.urlFile + "_Data.CSV");
            LoadGroups();
        }

        private void txtNewURL_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNewURL_GotFocus(Object sender, EventArgs e)
        {
            AcceptButton = btnAddNew;
        }

        private void txtNewTag_GotFocus(Object sender, EventArgs e)
        {
            AcceptButton = btnAddNew;
        }

        private void txtTag__GotFocus(Object sender, EventArgs e)
        {
            AcceptButton = btnSave;
        }

    }
}
