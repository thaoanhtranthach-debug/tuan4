namespace tuan4.Models
{
    public class Todo
    {
        public int Id { get; set; }           // Mã công việc
        public string TaskName { get; set; }  // Tên công việc
        public bool IsCompleted { get; set; } // Trạng thái hoàn thành
    }
}
