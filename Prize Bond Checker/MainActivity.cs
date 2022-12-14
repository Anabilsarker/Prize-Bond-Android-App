using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.CardView.Widget;
using AndroidX.Core.View;
using AndroidX.DrawerLayout.Widget;
using Google.Android.Material.FloatingActionButton;
using Google.Android.Material.Navigation;
using Prize_Bond_Checker.Database;
using System;
using Xamarin.Essentials;

namespace Prize_Bond_Checker
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
    {
        EditText bondNumber;
        CardView addNewBondLayout;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            InitUIEvents();

            //Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            //SetSupportActionBar(toolbar);
            //DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            //ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            //drawer.AddDrawerListener(toggle);
            //toggle.SyncState();
        }
        public override void OnBackPressed()
        {
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            if (drawer.IsDrawerOpen(GravityCompat.Start))
            {
                drawer.CloseDrawer(GravityCompat.Start);
            }
            else
            {
                base.OnBackPressed();
            }
        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private async void InitUIEvents()
        {
            addNewBondLayout = FindViewById<CardView>(Resource.Id.add_bond_popup);
            bondNumber = FindViewById<EditText>(Resource.Id.bond_number_entry_field);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;

            Button addnewbond = FindViewById<Button>(Resource.Id.add_bond);
            addnewbond.Click += AddNewbond_Click;

            Button cancel = FindViewById<Button>(Resource.Id.cancel);
            cancel.Click += Cancel_Click;

            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener(this);

            ListView listView = FindViewById<ListView>(Resource.Id.bondNumbertListView);
            CustomAdapter customAdapter = new CustomAdapter(await SQLiteDatabase.GetBonds(), this.ApplicationContext);
            listView.Adapter = customAdapter;
        }
        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            CardView addNewBondLayout = FindViewById<CardView>(Resource.Id.add_bond_popup);
            addNewBondLayout.Visibility = ViewStates.Visible;

            TextView textView = FindViewById<TextView>(Resource.Id.help_info);
            textView.Visibility = ViewStates.Gone;
        }
        private async void AddNewbond_Click(object sender, EventArgs e)
        {
            QueryBonds queryBonds = new QueryBonds();
            if (await queryBonds.PostRequest(bondNumber.Text))
            {
                Android.App.AlertDialog.Builder dialog = new Android.App.AlertDialog.Builder(this);
                Android.App.AlertDialog result = dialog.Create();
                result.SetTitle("Result");
                result.SetMessage("Match Found: " + queryBonds.BondResult());
                result.SetButton("OK", (c, ev) =>
                {
                    result.Cancel();
                });
                result.Show();
            }
            if (!await queryBonds.ParseInput(bondNumber.Text))
                InvalidInput();
        }
        private void Cancel_Click(object sender, EventArgs e)
        {
            bondNumber.Text = "";
            addNewBondLayout.Visibility = ViewStates.Gone;
        }
        private void InvalidInput()
        {
            Android.App.AlertDialog.Builder dialog = new Android.App.AlertDialog.Builder(this);
            Android.App.AlertDialog result = dialog.Create();
            result.SetTitle("Error");
            result.SetMessage("Incorrect Entry");
            result.SetButton("OK", (c, ev) =>
            {
                result.Cancel();
            });
            result.Show();
        }
        public bool OnNavigationItemSelected(IMenuItem item)
        {
            int id = item.ItemId;

            //if (id == Resource.Id.nav_camera)
            //{
            //    // Handle the camera action
            //}
            //else if (id == Resource.Id.nav_gallery)
            //{

            //}
            //else if (id == Resource.Id.nav_slideshow)
            //{

            //}
            //else if (id == Resource.Id.nav_manage)
            //{

            //}
            //else if (id == Resource.Id.nav_share)
            //{

            //}
            //else if (id == Resource.Id.nav_send)
            //{

            //}

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            drawer.CloseDrawer(GravityCompat.Start);
            return true;
        }
    }
}

