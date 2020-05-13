using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace MvcApplication1.Models
{
    public class Person
    {
        //姓名
        [Required]
        [Display(Name = "姓名")]
        [StringLength(4, ErrorMessage = "请输入正确的姓名格式！", MinimumLength = 2)]
        public String name { get; set; }
        //账号
        [Required]
        [Display(Name = "账号")]
        [StringLength(12,ErrorMessage = "请输出正确格式的账号！",MinimumLength = 11)]
        public String number { get; set; }
        //密码
        [Required]
        [DataType(DataType.Password)]
        [StringLength(30, ErrorMessage = "密码请大于5位，小于30位", MinimumLength = 6)]
        [Display(Name = "密码")]
        public String password { get; set; }
        //确认密码
        [Required]
        [Display(Name = "确认密码")]
        [Compare("password", ErrorMessage = "密码和确认密码不匹配。")]
        public string confirmPassword { get; set; }
        //记住密码
        [Display(Name = "记住密码？")]
        public bool Remember { get; set; }
        //登录类型
        public int type { get; set; }
        //个人备注信息
        public int info { get; set; }
    }
}
