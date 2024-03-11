﻿namespace LMS.DTOs
{
    public class IssueBookFormRequestDto
    {
        public string ISBN { get; set; }
    }
    public class IssueBookFormResponseDto
    {
        public string ISBN { get; set; }
        public string URL { get; set; }
    }

    public class IssueBookRequestDto
{
        public string ISBN { get; set; }
        public string BorrowerID { get; set; }
        public string IssuedID {  get; set; }
        public string DueDate { get; set; }
    }
    public class IssueBookResponseDto
{
        public int ReservationId {  get; set; }
        public string ISBN { get; set; }
        public string BorrowerId {  get; set; } 
    }
}