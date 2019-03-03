package md533ba7b5c07c04bddd731780997f3c845;


public class XfxCardViewRendererDroid
	extends android.support.v7.widget.CardView
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onTouchEvent:(Landroid/view/MotionEvent;)Z:GetOnTouchEvent_Landroid_view_MotionEvent_Handler\n" +
			"";
		mono.android.Runtime.register ("Xfx.Controls.Droid.Renderers.XfxCardViewRendererDroid, Xfx.Controls.Droid", XfxCardViewRendererDroid.class, __md_methods);
	}


	public XfxCardViewRendererDroid (android.content.Context p0)
	{
		super (p0);
		if (getClass () == XfxCardViewRendererDroid.class)
			mono.android.TypeManager.Activate ("Xfx.Controls.Droid.Renderers.XfxCardViewRendererDroid, Xfx.Controls.Droid", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
	}


	public XfxCardViewRendererDroid (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == XfxCardViewRendererDroid.class)
			mono.android.TypeManager.Activate ("Xfx.Controls.Droid.Renderers.XfxCardViewRendererDroid, Xfx.Controls.Droid", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
	}


	public XfxCardViewRendererDroid (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == XfxCardViewRendererDroid.class)
			mono.android.TypeManager.Activate ("Xfx.Controls.Droid.Renderers.XfxCardViewRendererDroid, Xfx.Controls.Droid", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public boolean onTouchEvent (android.view.MotionEvent p0)
	{
		return n_onTouchEvent (p0);
	}

	private native boolean n_onTouchEvent (android.view.MotionEvent p0);

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
