using System;

namespace Confluent.Kafka
{
    /// <summary>
    ///     A key/value pair to be sent to Kafka. This consists of a topic name to which the record is being sent, an optional partition number, and an optional key and value.
    ///     If a valid partition number is specified that partition will be used when sending the record. If no partition is specified but a key is present a partition will be chosen using a hash of the key. If neither key nor partition is present a partition will be assigned in a round-robin fashion.
    ///     The record also has an associated timestamp. If the user did not provide a timestamp, the producer will stamp the record with its current time. The timestamp eventually used by Kafka depends on the timestamp type configured for the topic.
    /// </summary>
    public struct ProducerRecord<TKey, TValue>
    {
        /// <summary>
        ///     Creates a record with a specified timestamp to be sent to a specified topic and partition.
        /// </summary>
        /// <param name="topic">
        ///     The topic the record will be appended to.
        /// </param>
        /// <param name="value">
        ///     The record contents.
        /// </param>
        public ProducerRecord(string topic, TValue value)
            : this(topic, default(TKey), value, Producer.RD_KAFKA_PARTITION_UA, null)
        {
        }

        /// <summary>
        ///     Creates a record with a specified timestamp to be sent to a specified topic and partition.
        /// </summary>
        /// <param name="topic">
        ///     The topic the record will be appended to.
        /// </param>
        /// <param name="key">
        ///     The key that will be included in the record.
        /// </param>
        /// <param name="value">
        ///     The record contents.
        /// </param>
        public ProducerRecord(string topic, TKey key, TValue value)
            : this(topic, key, value, Producer.RD_KAFKA_PARTITION_UA, null)
        {
        }

        /// <summary>
        ///     Creates a record with a specified timestamp to be sent to a specified topic and partition.
        /// </summary>
        /// <param name="topic">
        ///     The topic the record will be appended to.
        /// </param>
        /// <param name="key">
        ///     The key that will be included in the record.
        /// </param>
        /// <param name="value">
        ///     The record contents.
        /// </param>
        /// <param name="partition">
        ///     The partition to which the record should be sent.
        /// </param>
        public ProducerRecord(string topic, TKey key, TValue value, int partition) 
            : this(topic, key, value, partition, null)
        {
        }

        /// <summary>
        ///     Creates a record with a specified timestamp to be sent to a specified topic and partition.
        /// </summary>
        /// <param name="topic">
        ///     The topic the record will be appended to.
        /// </param>
        /// <param name="key">
        ///     The key that will be included in the record.
        /// </param>
        /// <param name="value">
        ///     The record contents.
        /// </param>
        /// <param name="partition">
        ///     The partition to which the record should be sent.
        /// </param>
        /// <param name="timestamp">
        ///     The timestamp of the record.
        /// </param>
        public ProducerRecord(string topic, TKey key, TValue value, int partition, DateTime? timestamp)
        {
            Topic = topic;
            Partition = partition;
            Timestamp = timestamp;
            Key = key;
            Value = value;
        }

        /// <summary>
        /// The topic the record will be appended to.
        /// </summary>
        public string Topic { get; }

        /// <summary>
        /// The key that will be included in the record.
        /// </summary>
        public TKey Key { get; }

        /// <summary>
        /// The record contents.
        /// </summary>
        public TValue Value { get; }

        /// <summary>
        /// The partition to which the record should be sent.
        /// </summary>
        public int Partition { get; }

        /// <summary>
        /// The timestamp of the record.
        /// </summary>
        public DateTime? Timestamp { get; }
    }
}
