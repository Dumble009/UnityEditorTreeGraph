
namespace BT
{
	public delegate Timing TimingCreate();
	public class BT_Timing : BT_Node
	{
		public BT_Timing(BehaviourTree _tree, bool _isOverwrite, bool _isMultiple, string _name) : base()
		{
			tree = _tree;
			isOverwrite = _isOverwrite;
			isMultiple = _isMultiple;
			name = _name;
		}
		
		protected BehaviourTree tree;
		protected bool isOverwrite;
		protected bool isMultiple;
		protected string name;
		public Timing lastInstance;
		protected TimingCreate timingCreator;

		public override ResultContainer Next()
		{
			if (tree != null)
			{
				if (lastInstance == null || lastInstance.IsActivated || isMultiple)
				{
					lastInstance = CreateNewTiming();
					lastInstance.Name = name;
					lastInstance.Init();
					tree.AddTiming(lastInstance);
				}
				else
				{
					if (isOverwrite)
					{
						lastInstance.Init();
					}
				}
			}
			if (children != null && children.Count > 0 && children[0] != null)
			{
				ResultContainer result = children[0].Next();
				return result;
			}
			return base.Next();
		}

		public void SetTimingCreator(TimingCreate creator)
		{
			timingCreator = creator;
		}

		virtual protected Timing CreateNewTiming()
		{
			if (timingCreator != null)
			{
				return timingCreator.Invoke();
			}
			else
			{
				return new Timing(null);
			}
		}
	}

	public class Timing
	{
		protected BT_Node target;
		bool isActivated;
		protected string name;
		public string Name
		{
			get
			{
				return name;
			}
			set
			{
				name = value;
			}
		}
		public bool IsActivated {
			get {
				return isActivated;
			}
		}
		public Timing(BT_Node node)
		{
			target = node;
			isActivated = false;
		}
		virtual public void Init()
		{

		}

		virtual public bool Check()
		{
			return true;
		}

		virtual public ResultContainer Next()
		{
			isActivated = true;
			if (target != null)
			{
				return target.Next();
			}
			return new ResultContainer(BT_Result.SUCCESS);
		}
	}
}