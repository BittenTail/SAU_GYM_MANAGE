using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;


namespace MvcApplication1.Models
{
    public class Venue
    {
        public int id { get; set; }
        //类型
        public int type { get; set; }
        //状态
        [Required]
        [Display(Name = "状态")]
        public String state { get; set; }
        //单价
        [Required]
        [Display(Name = "单价")]
        [StringLength(12, ErrorMessage = "请输出正确的单价账号！", MinimumLength = 11)]
        public String price { get; set; }
        //最大人数
        [Required]
        [Display(Name = "最大人数")]
        public String maxnumber { get; set; }
        //当前人数
        [Required]
        [Display(Name = "当前人数")]
        public string nownumber { get; set; }
        [Required]
        [Display(Name = "类型名")]
        [StringLength(5, ErrorMessage = "请输入正确的名称格式！", MinimumLength = 2)]
        public String typename { get; set; }
       
        public String typeimage { get; set; }
       
    }
}