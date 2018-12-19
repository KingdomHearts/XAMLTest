using Android.App;
using Android.Content;
using Android.Database;
using Android.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Java.Util;
using Android;
using Android.Widget;

namespace XAMLTest.Views.ApiPages
{
	public class Calendar2 : ListActivity
    {
        public Calendar2 ()
		{
            /*
            CursorLoader cursorLoader = new CursorLoader(this);
            var cursor = cursorLoader.LoadInBackground();


            var calendarsUri = CalendarContract.Calendars.ContentUri;
            string[] calendarsProjection = {
            CalendarContract.Calendars.InterfaceConsts.Id,
            CalendarContract.Calendars.InterfaceConsts.CalendarDisplayName,
            CalendarContract.Calendars.InterfaceConsts.AccountName
            };

            var loader = new CursorLoader(this, calendarsUri, calendarsProjection, null, null, null);

            var test = loader.LoadInBackground();

            string[] sourceColumns = {
            CalendarContract.Calendars.InterfaceConsts.CalendarDisplayName,
            CalendarContract.Calendars.InterfaceConsts.AccountName };

            int[] targetResources = {
            Resource.Id.calDisplayName,  };

            SimpleCursorAdapter adapter = new SimpleCursorAdapter(this,
                Resource.Layout.CalListItem, cursor, sourceColumns, targetResources);

            ListAdapter = adapter;*/
        }
	}
}