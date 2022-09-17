using System;

namespace Backend.Models
{
    public class Sensor
    {
        public Guid Id { get; set; }
        public Node Node { get; set; }
        public SensorType SensorType { get; set; }
        public TimeSeries TimeSeries { get; set; }

        public Sensor(Guid id, Node node, SensorType sensorType, TimeSeries timeSeries)
        {
            Id = id;
            Node = node;
            SensorType = sensorType;
            TimeSeries = timeSeries;
        }
    }

}
