using System;

namespace ICUMonitoringSystem
{
   
    public struct VitalRecord
    {
        public int HeartRate;      
        public int SpO2;           
        public int SystolicBP;    
        public int DiastolicBP;    
        public string Timestamp;

        public VitalRecord(int heartRate, int spO2, int systolicBP, int diastolicBP, string timestamp)
        {
            HeartRate = heartRate;
            SpO2 = spO2;
            SystolicBP = systolicBP;
            DiastolicBP = diastolicBP;
            Timestamp = timestamp;
        }

        public void Display()
        {
            Console.WriteLine($"[{Timestamp}] HR: {HeartRate} BPM | SpO2: {SpO2}% | BP: {SystolicBP}/{DiastolicBP} mmHg");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            VitalRecord[] patientData = new VitalRecord[]
            {
                new VitalRecord(72, 98, 118, 78, "08:00"),
                new VitalRecord(75, 97, 120, 80, "08:01"),
                new VitalRecord(88, 96, 125, 82, "08:02"),
                new VitalRecord(105, 94, 135, 88, "08:03"), 
                new VitalRecord(112, 92, 142, 92, "08:04"), 
                new VitalRecord(110, 91, 140, 90, "08:05"),
                new VitalRecord(95, 93, 130, 85, "08:06"),
                new VitalRecord(82, 96, 122, 80, "08:07"),
                new VitalRecord(76, 98, 119, 79, "08:08"),
                new VitalRecord(70, 99, 115, 75, "08:09")
            };


            Console.WriteLine("--- ICU PATIENT MONITORING SYSTEM-----");
   

            Console.WriteLine("--- Patient Vitals Log ---");
            for (int i = 0; i < patientData.Length; i++)
            {
                patientData[i].Display();
            }


            DetectAnomalies(patientData);

            CalculateMovingAverage(patientData, windowSize: 5);

            Console.WriteLine("\n--- Search Record by Timestamp ---");
            string targetTime = "08:04";
            int foundIndex = SearchByTimestamp(patientData, targetTime);
            if (foundIndex != -1)
            {
                Console.Write($"Record found at index {foundIndex}: ");
                patientData[foundIndex].Display();
            }
            else
            {
                Console.WriteLine($"No record found for timestamp: {targetTime}");
            }

            Console.WriteLine("\n==================================================");
        }

        static void DetectAnomalies(VitalRecord[] records)
        {
            bool anomalyFound = false;

            for (int i = 0; i < records.Length; i++)
            {
                VitalRecord r = records[i];
                string alerts = "";

                if (r.HeartRate < 60) alerts += "[LOW HR] ";
                else if (r.HeartRate > 100) alerts += "[HIGH HR / TACHYCARDIA] ";

                if (r.SpO2 < 95) alerts += "[LOW SpO2 / HYPOXIA] ";

                if (r.SystolicBP > 140 || r.DiastolicBP > 90) alerts += "[HYPERTENSION ALERT] ";
                else if (r.SystolicBP < 90 || r.DiastolicBP < 60) alerts += "[HYPOTENSION ALERT] ";

                if (alerts.Length > 0)
                {
                    Console.WriteLine($"ALERT at {r.Timestamp} -> {alerts}");
                    anomalyFound = true;
                }
            }

            if (!anomalyFound)
            {
                Console.WriteLine("All vital signs are within normal parameters.");
            }
        }

        static void CalculateMovingAverage(VitalRecord[] records, int windowSize)
        {
            if (records.Length < windowSize)
            {
                Console.WriteLine("Not enough data to calculate sliding window average.");
                return;
            }

            double currentHRSum = 0;
            double currentSpO2Sum = 0;

            for (int i = 0; i < windowSize; i++)
            {
                currentHRSum += records[i].HeartRate;
                currentSpO2Sum += records[i].SpO2;
            }

            Console.WriteLine($"Window 1 [{records[0].Timestamp} to {records[windowSize - 1].Timestamp}] -> Avg HR: {(currentHRSum / windowSize):F1} BPM, Avg SpO2: {(currentSpO2Sum / windowSize):F1}%");

            for (int i = windowSize; i < records.Length; i++)
            {
 
                currentHRSum += records[i].HeartRate - records[i - windowSize].HeartRate;
                currentSpO2Sum += records[i].SpO2 - records[i - windowSize].SpO2;

                string startTime = records[i - windowSize + 1].Timestamp;
                string endTime = records[i].Timestamp;

                Console.WriteLine($"Window {i - windowSize + 2} [{startTime} to {endTime}] -> Avg HR: {(currentHRSum / windowSize):F1} BPM, Avg SpO2: {(currentSpO2Sum / windowSize):F1}%");
            }
        }

  
        static int SearchByTimestamp(VitalRecord[] records, string timestamp)
        {
            for (int i = 0; i < records.Length; i++)
            {
                if (records[i].Timestamp == timestamp)
                {
                    return i;
                }
            }
            return -1;
        }

   
        static void SortArray(int[] arr)
        {
            for (int i = 1; i < arr.Length; i++)
            {
                int key = arr[i];
                int j = i - 1;

                while (j >= 0 && arr[j] > key)
                {
                    arr[j + 1] = arr[j];
                    j--;
                }
                arr[j + 1] = key;
            }
        }


        
    }
}