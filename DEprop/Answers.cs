//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DEprop
{
    using System;
    using System.Collections.Generic;
    
    public partial class Answers
    {
        public int AnswerId { get; set; }
        public string AnswerName { get; set; }
        public int AnswerTF { get; set; }
        public int QuestionId { get; set; }
    
        public virtual Questions Questions { get; set; }
    }
}