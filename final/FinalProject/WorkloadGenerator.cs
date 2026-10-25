#nullable enable

public class WorkloadGenerator {
	public static List<Process> GetWorkloadOne() {
		var workload = new List<Process>();

		for (int i = 0; i < 3; i++) {
			workload.Add(new Process(i, 10, 10, () => { Console.WriteLine("Finished"); }));
		}

		return workload;
	}
}
