using Character_Builder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace EmbeddedUI
{
    public class EmbeddedUI
    {
        public static EndWizardPage wizPage { get; private set; }
        public static AboutWizardPage aboutPage { get; private set; }

		public static void App_Startup(object sender, StartupEventArgs e)
        {
			Application.Current.LoadCompleted += CurrentApp_LoadCompleted;
		}

        private static void CurrentApp_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
			Install((App)Application.Current);
			Application.Current.LoadCompleted -= CurrentApp_LoadCompleted;

		}

		public static void Install(App app)
        {
			var window = app.win;
			WizardPage manageEnd = window.pages[3];
			WizardPage manageCenter = new WizardPage(manageEnd.Name, null, null, manageEnd.InfoSize, manageEnd.Visited, null);
			manageCenter.TabFlag = Visibility.Visible;
            manageEnd.TabFlag = Visibility.Hidden;
			foreach (WizardPage child_tab in manageEnd.child_tabs)
			{
				if (child_tab is StartWizardPage)
				{
                    manageCenter.child_tabs.Add(new StartWizardPage(child_tab.Name, child_tab.Page, child_tab.Action, child_tab.InfoSize, child_tab.Visited, manageCenter)
					{
						TabFlag = Visibility.Visible
					});
				}
				else if (child_tab is EndWizardPage)
				{
                    manageCenter.child_tabs.Add(new EndWizardPage(child_tab.Name, child_tab.Page, child_tab.Action, child_tab.InfoSize, child_tab.Visited, manageCenter)
					{
						TabFlag = Visibility.Visible
					});
				}
				else
				{
                    manageCenter.child_tabs.Add(new WizardPage(child_tab.Name, child_tab.Page, child_tab.Action, child_tab.InfoSize, child_tab.Visited, manageCenter)
					{
						TabFlag = Visibility.Visible
					});
				}
			}
			window.pages.Remove(manageEnd);
			window.pages.Add(manageCenter);
			window.pages.Add(wizPage = new EndWizardPage("CBLoader", null, null, window.small_infosize, tabflag: false, null));
			wizPage.child_tabs.Add(new WizardPage("About", aboutPage = new AboutWizardPage(), null, window.small_infosize, tabflag: true, wizPage)
			{
				TabFlag = Visibility.Visible
			});
			wizPage.TabFlag = Visibility.Visible;
			wizPage.child_tabs[0].TabFlag = Visibility.Visible;
			((System.Windows.Controls.ListBox)window.FindName("NavButtons")).ItemsSource = window.pages.ToArray();
		}
    }
}
