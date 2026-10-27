#nullable enable

public class MemoryManager {
	private int _total_mem;
	private int _used_mem = 0;

	private HashSet<int> _allocated_procs = new HashSet<int>();
	
	public MemoryManager(int total_mem) {
		_total_mem = total_mem;
	}
	
	// Attempts to allocate the memory needed for a process.
	// Returns true on success, false on failure.
	public bool Alloc(Process proc) {
		if (_allocated_procs.Contains(proc.GetID())) {
			throw new InvalidOperationException("Attempted to realloc a process");
		}

		int mem = proc.GetMem();
		int avail = GetAvailableMemory();

		if (mem > _total_mem) {
			throw new ArgumentOutOfRangeException($"Cannot schedule a process with memory requirement greater than max memory! ({mem}>{_total_mem})");
		}

		if (mem <= avail) {
			_used_mem += proc.GetMem();
			return _allocated_procs.Add(proc.GetID());
		} else {
			return false;
		}
	}
	
	// Attempts to free a given process from memory.
	// Returns false if the process wasn't already allocated.
	public bool FreeProc(Process proc) {
		int pid = proc.GetID();
		int mem = proc.GetMem();
		return _allocated_procs.Remove(pid);
	}
	
	// Using Alloc will return false if not enough memory, don't use this
	private int GetAvailableMemory() {
		return _total_mem - _used_mem;
	}
}
