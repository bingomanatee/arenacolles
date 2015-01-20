using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace AC
{
		public class ObservableListTest
		{
				class Foo : ObservedList<int>
				{
				
				}
				[Test]
	
				public void ChangedObservation ()
				{
						Foo foo = new Foo ();
						bool changeObserved = false;
						foo.ListChangedEvent += delegate {
								changeObserved = true;
						};
						
						Assert.AreEqual (changeObserved, false, "changeObserved initially is false");
						
						foo.Add (1);
						
						Assert.AreEqual (changeObserved, true, "delegate changes changeObserved");
			
				}
				
				[Test]
				
				public void ItemChangedObservation ()
				{
				
						Foo foo = new Foo ();
						int lastChanged = -1;
						foo.ItemChangedEvent += delegate(object sender, EventArg<ObservedList<int>.ListChangedData<int>> e) {
								lastChanged = e.CurrentValue.Index;
						};
				
						Assert.AreEqual (-1, lastChanged, "no initial data on changed");
						foo.Add (2);
						foo [0] = 1;
						
						Assert.AreEqual (1, foo [0], "change was successful");
						Assert.AreEqual (0, lastChanged, "first item changed (added)");
						
						foo.Add (4);
						foo [1] = 2;
						Assert.AreEqual (1, lastChanged, "second item changed (added)");
				}
		}
	
}