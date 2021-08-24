namespace kassets
{  
	public class Reference
	{
        public int refCount;
		public bool IsUnused ()
		{
			return refCount <= 0;
		}

		public virtual void Retain ()
		{
			refCount++;
		}

		public virtual void Release ()
		{
			refCount--;
		} 
	} 
}
