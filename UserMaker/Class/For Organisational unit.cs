﻿using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserMaker.Class
{
	internal class For_Organisational_unit
	{
		public string[] Load_OU()
		{
			// Specify the LDAP path of the root organizational unit
			string rootOUPath = "LDAP://OU=Users,OU=Accounts,DC=internal,DC=detmold,DC=com,DC=au";

			// Create a DirectoryEntry object for the root OU
			using (DirectoryEntry rootOU = new DirectoryEntry(rootOUPath))
			{
				// Create a DirectorySearcher object to search within the root OU
				using (DirectorySearcher searcher = new DirectorySearcher(rootOU))
				{
					// Filter to find only organizational units (OU)
					searcher.Filter = "(objectClass=organizationalUnit)";

					// Perform the search
					SearchResultCollection results = searcher.FindAll();

					string[] organizationalUnits = new string[results.Count];
					for (int i = 0; i < results.Count; i++)
					{
						DirectoryEntry entry = results[i].GetDirectoryEntry();
						organizationalUnits[i] = entry.Name;
					}
					return organizationalUnits;
				}
			}
		}
	}
}
