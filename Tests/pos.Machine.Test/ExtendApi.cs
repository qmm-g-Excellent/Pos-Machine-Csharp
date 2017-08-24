using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace pos.Machine.Test
{
    public class ExtendApi
    {   
        [Fact]
        public void shoul_filtering_elements_using_where()
        {
//            use Where() to filter element, the return of Where() execute isn't a array,so it's need to use ToArray()  to generate array,
//             the most  important is traverse every element to execute that filter fuction of parameter provide just when execute ToArray() function,
            int[] data = { 1, 2, 3, 4, 5, 6, 8};
            int[] result = data.Where(n => n % 2 == 0).ToArray();
            int[] expextedResult = {2, 4, 6, 8};
            Assert.Equal(expextedResult,result);
        }

        [Fact]
        public void should_resoleve_mapping_using_select()
        {
            int[] scores = {45, 56, 63, 76, 83};
            //its first params just is index of colection
            //用于处理映射，返回另一种类型的集合，原来集合有多少个元素，则返回的结果依然还是多少个元素
            IEnumerable<String> levels = scores.Select(score => score > 60 ? "PASS" : "REJECT").ToArray().ToArray();
            string[] expectedResult = {"REJECT", "REJECT", "PASS", "PASS", "PASS"};
            Assert.Equal(expectedResult, levels);
            //select extend function return type is IEnumerable  //!!!!!!!!!!!! ---> import !!!!
        }

        [Fact]
        public void should_get_sum_using_aggregate_with_no_first_param()
        {
            int[] scores = { 2, 2, 4 };
            int sum = scores.Aggregate((total, score) =>  total + score);//做聚合，
            //如果第一个不写，默认为第一个元素。
            int expectResult = 8;
            Assert.Equal(expectResult, sum);
        }

        [Fact]
        public void should_get_sum_using_aggregate()
        {
            int[] scores = {1, 2, 4};
            int sum = scores.Aggregate(2, (total, score) =>  total + score);// 第一个参数初始值，如果不写第一个参数，就默认集合中第一个参数是起始值，从第二个元素开始相加
            const int expectResult = 9;
            Assert.Equal(expectResult, sum);
        }

        [Fact]
        public void should_get_group_using_groupBy()
        {
            List<Student> studentList = new List<Student>
            {
                new Student {ClassName = "软工一班", StudentName = "张三", StuId = 1},
                new Student {ClassName = "软工一班", StudentName = "李四", StuId = 2},
                new Student {ClassName = "软工一班", StudentName = "王五", StuId = 3},
                new Student {ClassName = "软工二班", StudentName = "范冰冰", StuId = 4},
                new Student {ClassName = "软工二班", StudentName = "杨幂", StuId = 5},
                new Student {ClassName = "软工二班", StudentName = "林依晨", StuId = 6},
            };
            var studentGroup = studentList.GroupBy(s => s.ClassName);
            var className = studentGroup.First(s => s.Key == "软工一班").ToArray();
//            foreach (IGrouping<string, Student> item in studentGroup)
//            {
//                Console.WriteLine(item.Key);
//                foreach (var student in item)
//                {
//                    Console.WriteLine(student.StudentName);
//                }
//            }

            Console.WriteLine(studentGroup);
            Assert.Equal("张三",className[0].StudentName);
        }
    }

}