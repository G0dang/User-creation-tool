﻿using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserMaker.Class
{
	internal class For_RegionalManager
	{
		public UserInformation[] Load_RM()
		{
			List<UserInformation> managers = new List<UserInformation>();
			string rootOUPath = "LDAP://OU=,OU=,DC=,DC=,DC=,DC=";

			using (DirectoryEntry rootOU = new DirectoryEntry(rootOUPath))
			using (DirectorySearcher searcher = new DirectorySearcher(rootOU))
			{
				searcher.Filter = "(&(objectClass=user)(objectCategory=person)(!userAccountControl:1.2.840.113556.1.4.803:=2))";
				searcher.PropertiesToLoad.Add("displayName");
				searcher.PropertiesToLoad.Add("distinguishedName");
				searcher.PropertiesToLoad.Add("samaccountname");
				SearchResultCollection results = searcher.FindAll();

				foreach (SearchResult result in results)
				{
					DirectoryEntry entry = result.GetDirectoryEntry();

					UserInformation user = new UserInformation();

					if (entry.Properties.Contains("displayName"))
					{
						user.DisplayName = entry.Properties["displayName"].Value.ToString();
					}

					//the distinguished name is created by the directory service based on
					//the organizational structure that have been defined in the directory,
					//and it reflects the hierarchical location of each user within that structure.

					if (entry.Properties.Contains("distinguishedName"))
					{
						user.DistinguishedName = entry.Properties["distinguishedName"].Value.ToString();
					}

					if (entry.Properties.Contains("samaccountname"))
					{
						user.SamAccountName = entry.Properties["samaccountname"].Value.ToString();
					}

					managers.Add(user);
				}
			}

			return managers.ToArray();

		}
	}
}
