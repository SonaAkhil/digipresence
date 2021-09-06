package crc644a8af360951039ac;


public class HeaderViewHolder
	extends android.support.v7.widget.RecyclerView.ViewHolder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("DigiPresence.HeaderViewHolder, DigiPresence", HeaderViewHolder.class, __md_methods);
	}


	public HeaderViewHolder (android.view.View p0)
	{
		super (p0);
		if (getClass () == HeaderViewHolder.class)
			mono.android.TypeManager.Activate ("DigiPresence.HeaderViewHolder, DigiPresence", "Android.Views.View, Mono.Android", this, new java.lang.Object[] { p0 });
	}

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
