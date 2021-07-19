class Task
{
    private DateTime targetDate;
    private bool isCompleted;
    //private bool isParentTask; // ����� �� ������������ ��� �����-���� ������
    //private Task parentTask;
    //private float percentOfProgress;
    public static List<Task> TaskList = new List<Task>();

    public string Name { get; set; }

    public string Description { get; set; }

    public DateTime TargetDate
    {
        get { return targetDate; }
        set
        {

            DateTime today = DateTime.Today;
            TimeSpan days = TargetDate - today;
            if (days.TotalDays < 0)
            {
                throw new ArgumentOutOfRangeException("�������� ����. ������ ��� ������ ���� �������."); //
            }
            else
            {
                targetDate = TargetDate;
            }
        }
    }

    /*public Task ParentTask
    {
        get { return parentTask; }
        set {
            if (ParentTask.isParentTask)
            {
                parentTask = ParentTask;
            }
            else { throw new Exception("������ �� �������� ������������!"); } //
        }
    }*/

    //���� ��������� ������ �������� ������:
    public Task(string name, string descr, DateTime trgtDate)
    {
        Name = name;
        Description = descr;
        //TargetDate = trgtDate;
        DateTime today = DateTime.Today;
        TimeSpan days = trgtDate - today;
        if (days.TotalDays < 0)
        {
            throw new ArgumentOutOfRangeException("�������� ����. ������ ��� ������ ���� �������."); //
        }
        else
        {
            targetDate = trgtDate;
        }
        TaskList.Add(this);
    }

    /*���� ��������� ������� �������� ������:
    public Task(string name, string description, DateTime targetDate, Task parentTask)
    {
        Name = name;
        Description = description;
        TargetDate = targetDate;
        ParentTask = parentTask;
    }*/

    public void TaskComplited(Task task)
    {
        task.isCompleted = true;
    }
}
