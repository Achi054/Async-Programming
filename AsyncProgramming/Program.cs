// Lets make tea
//MakeTea();
//await MakeTeaAsync();
//MakeCall();
//await MakeCallAsync();

#region Synchronous
void MakeTea()
{
    BoilWater();

    BoilMilk();

    TakeCup();

    PourTea();

    PourMilk();
}

void PourMilk()
{
    Console.WriteLine("Pouring milk to cup.");
}

void PourTea()
{
    Console.WriteLine("Pouring tea to cup");
}

void TakeCup()
{
    Console.WriteLine("Taking cup off shelve.");
}

void BoilMilk()
{
    Console.WriteLine("Boiling milk.");

    Task.Delay(3000).GetAwaiter().GetResult();

    Console.WriteLine("Milk ready");
}

void BoilWater()
{
    Console.WriteLine("Boiling tea leaves.");

    Task.Delay(3000).GetAwaiter().GetResult();

    Console.WriteLine("Decoction ready");
}
#endregion

#region Asynchronous
async Task MakeTeaAsync()
{
    var boiledWaterTask = BoilWaterAsync();

    var boildMilkTask = BoilMilkAsync();

    await TakeCupAsync();

    await boiledWaterTask;

    await PourTeaAsync();

    await boildMilkTask;

    await PourMilkAsync();
}

async Task PourMilkAsync()
{
    Console.WriteLine("Pouring milk to cup.");
}

async Task PourTeaAsync()
{
    Console.WriteLine("Pouring tea to cup");
}

async Task TakeCupAsync()
{
    Console.WriteLine("Taking cup off shelve.");
}

async Task BoilMilkAsync()
{
    Console.WriteLine("Boiling milk.");

    await Task.Delay(3000);

    Console.WriteLine("Milk ready");
}

async Task BoilWaterAsync()
{
    Console.WriteLine("Boiling tea leaves.");

    await Task.Delay(2000);

    Console.WriteLine("Decoction ready");
}
#endregion

#region Thread
void MakeCall()
{
    Console.WriteLine($"Task 1: Thread {Thread.CurrentThread.ManagedThreadId}");
    HttpClient client = new();

    Console.WriteLine($"Task 2: Thread {Thread.CurrentThread.ManagedThreadId}");
    var clientRef = client.GetStringAsync("http://google.com");

    Console.WriteLine($"Task 3: Thread {Thread.CurrentThread.ManagedThreadId}");
    int data = 0;
    for (int i = 0; i < 1000; i++)
    {
        data += i;
    }

    Console.WriteLine($"Task 4: Thread {Thread.CurrentThread.ManagedThreadId}");
    clientRef.GetAwaiter().GetResult();

    Console.WriteLine($"Task 5: Thread {Thread.CurrentThread.ManagedThreadId}");
}

async Task MakeCallAsync()
{
    Console.WriteLine($"Task 1: Thread {Thread.CurrentThread.ManagedThreadId}");
    HttpClient client = new();

    Console.WriteLine($"Task 2: Thread {Thread.CurrentThread.ManagedThreadId}");
    var clientTask = client.GetStringAsync("http://google.com");

    Console.WriteLine($"Task 3: Thread {Thread.CurrentThread.ManagedThreadId}");
    int data = 0;
    for (int i = 0; i < 1000; i++)
    {
        data += i;
    }

    Console.WriteLine($"Task 4: Thread {Thread.CurrentThread.ManagedThreadId}");
    await clientTask;

    Console.WriteLine($"Task 5: Thread {Thread.CurrentThread.ManagedThreadId}");
}
#endregion