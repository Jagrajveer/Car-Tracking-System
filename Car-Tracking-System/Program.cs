namespace Car_Tracking_System; 

class Program {
    static void Main(string[] args) {
        VehicleTracker vehicleTracker = new VehicleTracker(2, "123 Fake St.");
        Vehicle vehicle1 = new Vehicle("A01 T22", true);
        Vehicle vehicle2 = new Vehicle("A01 T21", false);

        vehicleTracker.AddVehicle(vehicle1);
        vehicleTracker.AddVehicle(vehicle2);
        
        
    }
}

public class Vehicle {
    public string Licence { get; set; }
    public bool Pass { get; set; }
    public Vehicle(string licence, bool pass) {
        this.Licence = licence;
        this.Pass = pass;
    }
}

public class VehicleTracker {
    //PROPERTIES
    public string Address { get; set; }
    public int Capacity { get; set; }
    public int SlotsAvailable { get; set; }
    public Dictionary<int, Vehicle> VehicleList { get; set; }

    public VehicleTracker(int capacity, string address) {
        this.Capacity = capacity;
        this.Address = address;
        this.VehicleList = new Dictionary<int, Vehicle>();
        this.SlotsAvailable = capacity;

        this.GenerateSlots();
    }

    // STATIC PROPERTIES
    public static string BadSearchMessage = "Error: Search did not yield any result.";
    public static string BadSlotNumberMessage = "Error: No slot with number ";
    public static string SlotsFullMessage = "Error: no slots available.";

    // METHODS
    public void GenerateSlots() {
        for (int i = 1; i <= this.Capacity; i++) {
            this.VehicleList.Add(i, null);
        }
    }

    public void AddVehicle(Vehicle vehicle) {
        foreach (KeyValuePair<int, Vehicle> slot in this.VehicleList) {
            if (slot.Value == null) {
                this.VehicleList[slot.Key] = vehicle;
                this.SlotsAvailable--;
                return;
            }
        } 
        throw new IndexOutOfRangeException(SlotsFullMessage);
    }

    public void RemoveVehicle(string licence) {
        try {
            int slot = this.VehicleList.First(v => v.Value.Licence == licence).Key;
            RemoveVehicle(slot);
        } catch {
            throw new NullReferenceException(BadSearchMessage);
        }
    }

    public bool RemoveVehicle(int slotNumber) {
        if (slotNumber > this.Capacity || slotNumber < 0) {
            return false;
        }
        this.VehicleList[slotNumber] = null;
        this.SlotsAvailable++;
        return true;
    }

    public List<Vehicle> ParkedPassholders() {
        List<Vehicle> passHolders = this
            .VehicleList
            .Where(v => v.Value.Pass)
            .Select(v=>v.Value)
            .ToList();
        
        return passHolders;
    }

    public int PassholderPercentage() {
        int passHolders = ParkedPassholders().Count();
        int percentage = passHolders * 100 / this.Capacity;
        return percentage;
    }
}
