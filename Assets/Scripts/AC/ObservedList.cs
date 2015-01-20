using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace AC
{
		[Serializable]
		public class ObservedList<T> : List<T>
		{
	
				public class ListChangedData<T>
				{
						ObservedList<T>	m_List;

						public ObservedList<T> List {
								get {
										return m_List;
								}
						}

						int m_index;

						public int Index {
								get {
										return m_index;
								}
						}
						
						T m_item;

						public T Item {
								get {
										return m_item;
								}
						}
			
						public ListChangedData (int index, T item, ObservedList<T> list)
						{
								m_index = index;
								m_List = list;
								m_item = item;
						}
	
				} 
	
				public event EventHandler<EventArg<ObservedList<T>>> ListChangedEvent;
				public event EventHandler<EventArg<ListChangedData<T>>> ItemChangedEvent;
   
				protected void Changed (int index, T value)
				{
						EventHandler<EventArg<ListChangedData<T>>> handler = ItemChangedEvent;
			
						if (handler != null) {
								T item = this [index];
								ListChangedData<T> arg = new  ListChangedData<T> (index, item, this);
								handler (this, new EventArg<ListChangedData<T>> (arg));
						}
				}

				protected void Updated ()
				{
						EventHandler<EventArg<ObservedList<T>>> handler = ListChangedEvent;
						
						if (handler != null) 
								handler (this, new EventArg<ObservedList<T>> (this));
				}
		
				public new void Add (T item)
				{
						base.Add (item);
						Updated ();
				}

				public new void Remove (T item)
				{
						base.Remove (item);
						Updated ();
				}

				public new void AddRange (IEnumerable<T> collection)
				{
						base.AddRange (collection);
						Updated ();
				}

				public new void RemoveRange (int index, int count)
				{
						base.RemoveRange (index, count);
						Updated ();
				}

				public new void Clear ()
				{
						base.Clear ();
						Updated ();
				}

				public new void Insert (int index, T item)
				{
						base.Insert (index, item);
						Updated ();
				}

				public new void InsertRange (int index, IEnumerable<T> collection)
				{
						base.InsertRange (index, collection);
						Updated ();
				}

				public new void RemoveAll (Predicate<T> match)
				{
						base.RemoveAll (match);
						Updated ();
				}
		
				public new T this [int index] {
						get {
								return base [index];
						}
						set {
								base [index] = value;
								Changed (index, value);
						}
				}
		
		
		}
	
}