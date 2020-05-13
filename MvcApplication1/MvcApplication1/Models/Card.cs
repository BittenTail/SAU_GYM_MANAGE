using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace MvcApplication1.Models
{
    public class Card
    {
        //卡号
        [Required]
        [Display(Name = "卡号")]
        public String cnumber { get; set; }
        //类别id
        [Required]
        [Display(Name = "类别id")]
        public String typeid { get; set; }
        //金额
        [Required]
        [Display(Name = "金额")]
        public String money { get; set; }
        //类型名
        [Required]
        [Display(Name = "类型名")]
        public String typename { get; set; }
        //折扣
        [Required]
        [Display(Name = "折扣")]
        public String state { get; set; }
    }
}