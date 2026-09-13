using System;

class Program {
    static void Main(string[] args) {
		Job job1 = new Job();
		job1._job_title = "Flavor Tester";
		job1._company = "Nabisco";
		job1._start_year = 2010;
		job1._end_year = 2018;

		Job job2 = new Job();
		job2._job_title = "CEO";
		job2._company = "Beijing Corn";
		job2._start_year = 2019;
		job2._end_year = 2026;

		Resume resume = new Resume();
		resume._name = "Buzz Blister";

		resume._jobs.Add(job1);
		resume._jobs.Add(job2);

		resume.Display();
    }
}
