using System;

namespace Confluent.Kafka
{
    public struct ProducerRecord<TKey, TValue>
    {
        public ProducerRecord(string topic, TValue value)
            : this(topic, default(TKey), value, Producer.RD_KAFKA_PARTITION_UA, null)
        {
        }

        public ProducerRecord(string topic, TKey key, TValue value)
            : this(topic, key, value, Producer.RD_KAFKA_PARTITION_UA, null)
        {
        }

        public ProducerRecord(string topic, TKey key, TValue value, int partition) 
            : this(topic, key, value, partition, null)
        {
        }

        public ProducerRecord(string topic, TKey key, TValue value, int partition, DateTime? timestamp)
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
