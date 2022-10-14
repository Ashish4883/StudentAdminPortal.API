using AutoMapper;
using Microsoft.AspNetCore.Mvc;

using StudentAdminPortal.API.DomainModels;
using StudentAdminPortal.API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAdminPortal.API.Controllers
{
    [ApiController]
    public class StudentsController : Controller
    {
        private readonly IStudentRepository studentRepository;
        private readonly IMapper mapper;

        public StudentsController(IStudentRepository studentRepository, IMapper mapper)
        {
            this.studentRepository = studentRepository;
            this.mapper = mapper;
        }

        //[HttpGet]
        //[Route("[controller]")]
        //public IActionResult GetAllStudents()
        //{
        //var students = studentRepository.GetStudents();
        //var domainModelStudents = new List<Student>();
        //foreach (var student in students)
        //{

        //This Code Before using Auto Mapper 

        //    domainModelStudents.Add( new Student()
        //    {
        //        Id = student.Id,
        //        FirstName = student.FirstName,
        //        LastName = student.LastName,
        //        DateOfBirth = student.DateOfBirth,
        //        Email = student.Email,
        //        Mobile = student.Mobile,
        //        ProfileImageUrl = student.ProfileImageUrl,
        //        GenderId = student.GenderId,
        //        Address = new Address()
        //        {
        //            Id = student.Address.Id,
        //            PhysicalAddress = student.Address.PhysicalAddress,
        //            PostalAddress = student.Address.PostalAddress
        //        },
        //        Gender = new Gender()
        //        {
        //            Id = student.Gender.Id,
        //            Description = student.Gender.Description
        //        }
        //    });
        //}
        //return Ok(domainModelStudents);            
        //}

        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> GetAllStudentsAsync()
        {
            //we are using automapper so don't need the above code
            //this line map the DataModel to DomainModel and return the value
            var students = await studentRepository.GetStudentsAsync();
            return Ok(mapper.Map<List<Student>>(students));
        }

        [HttpGet]
        [Route("[controller]/{studentId:guid}")]
        public async Task<IActionResult> GetStudentAsync([FromRoute] Guid studentId)
        {
            //Fetch single student details
            var student = await studentRepository.GetStudentAsync(studentId);

            //return details
            if (student == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<Student>(student));


        }

        [HttpPut]
        [Route("[controller]/{studentId:guid}")]
        public async Task<IActionResult> UpdateStudentAsync([FromRoute] Guid studentId, [FromBody] UpdateStudentRequest request)
        {
            if (await studentRepository.Exists(studentId))
            {
                //Update the Details
                var updatedStudent = await studentRepository.UpdateStudent(studentId, mapper.Map<DataModels.Student>(request));
                if (updatedStudent != null)
                {
                    return Ok(mapper.Map<Student>(updatedStudent));
                }
            }
            return NotFound();
        }




    }
}
