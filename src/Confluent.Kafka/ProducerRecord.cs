using System;

namespace Confluent.Kafka
{
    public struct ProducerRecord<TKey, TValue>
    {
        public ProducerRecord(string topic, TValue value)
            : this(topic, Producer.RD_KAFKA_PARTITION_UA, null, default(TKey), value)
        {
        }

        public ProducerRecord(string topic, TKey key, TValue value)
            : this(topic, Producer.RD_KAFKA_PARTITION_UA, null, key, value)
        {
        }

        public ProducerRecord(string topic, int partition, TKey key, TValue value) 
            : this(topic, partition, null, key, value)
        {
        }

        public ProducerRecord(string topic, int partition, DateTime? timestamp, TKey key, TValue value)
        {
            Topic = topic;
            Partition = partition;
            Timestamp = timestamp;
            Key = key;
            Value = value;
        }

        public string Topic { get; }

        public TKey Key { get; }

        public TValue Value { get; }

        public int Partition { get; set; }

        public DateTime? Timestamp { get; }
    }
}
