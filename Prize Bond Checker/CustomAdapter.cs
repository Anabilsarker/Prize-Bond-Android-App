using Android.Content;
using Android.Views;
using Android.Widget;
using Prize_Bond_Checker.Models;
using System;
using System.Collections.Generic;

namespace Prize_Bond_Checker
{
    public class CustomAdapter : BaseAdapter
    {
        List<Bonds> Bonds;
        string value;
        Context context;
        LayoutInflater layoutInflater;
        CheckedTextView simpleCheckedTextView;
        public override int Count => Bonds.Count;

        public CustomAdapter(List<Bonds> bonds, Context context)
        {
            Bonds = bonds;
            this.context = context;
            layoutInflater = LayoutInflater.From(context);
        }
        public override Java.Lang.Object GetItem(int position)
        {
            throw new NotImplementedException();
        }

        public override long GetItemId(int position)
        {
            return Bonds[position].BondNumber;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            convertView = layoutInflater.Inflate(Resource.Layout.bond_item_list, parent, false);
            simpleCheckedTextView = convertView.FindViewById<CheckedTextView>(Resource.Id.bondNumberCheckedTextView);
            simpleCheckedTextView.Text = Bonds[position].BondNumber.ToString();
            simpleCheckedTextView.Click += SimpleCheckedTextView_Click;
            return convertView;
        }
        private void SimpleCheckedTextView_Click(object sender, EventArgs e)
        {
            if (simpleCheckedTextView.Checked)
            {
                Console.WriteLine("Bond count:" + Count);
                // set cheek mark drawable and set checked property to false
                value = "un-Checked";
                simpleCheckedTextView.SetCheckMarkDrawable(Resource.Drawable.btn_checkbox_checked_to_unchecked_mtrl_animation);
                simpleCheckedTextView.Checked = false;
            }
            else
            {
                // set cheek mark drawable and set checked property to true
                value = "Checked";
                simpleCheckedTextView.SetCheckMarkDrawable(Resource.Drawable.btn_checkbox_unchecked_to_checked_mtrl_animation);
                simpleCheckedTextView.Checked = true;
            }
        }
    }
}