using System;
using System.Collections.Generic;

namespace Confluent.Kafka
{
    /// <summary>
    ///     A key/value pair to be sent to Kafka. This consists of a topic name to which the record is being sent, an optional partition number, and an optional key and value.
    ///     If a valid partition number is specified that partition will be used when sending the record. If no partition is specified but a key is present a partition will be chosen using a hash of the key. If neither key nor partition is present a partition will be assigned in a round-robin fashion.
    ///     The record also has an associated timestamp. If the user did not provide a timestamp, the producer will stamp the record with its current time. The timestamp eventually used by Kafka depends on the timestamp type configured for the topic.
    /// </summary>
    public class ProducerRecord<TKey, TValue>
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
            if (string.IsNullOrEmpty(topic))
                throw new ArgumentNullException(nameof(topic), "Topic cannot be null.");
            if (timestamp != null && timestamp.Value != DateTime.MinValue)
                throw new ArgumentException($"Invalid timestamp: {timestamp}. Timestamp should always be non-negative or null.");
            if (partition < 0)
                throw new ArgumentException($"Invalid partition: {partition}. Partition number should always be non-negative or null.");

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

        /// <summary>
        ///     Tests whether this <see cref="ProducerRecord{TKey, TValue}"/> instance is equal to the specified object.
        /// </summary>
        /// <param name="other">
        ///     The object to test.
        /// </param>
        protected bool Equals(ProducerRecord<TKey, TValue> other)
        {
            return string.Equals(Topic, other.Topic) && EqualityComparer<TKey>.Default.Equals(Key, other.Key) && EqualityComparer<TValue>.Default.Equals(Value, other.Value) && Partition == other.Partition && Timestamp.Equals(other.Timestamp);
        }

        /// <summary>
        ///     Tests whether this <see cref="ProducerRecord{TKey, TValue}"/> instance is equal to the specified object.
        /// </summary>
        /// <param name="obj">
        ///     The object to test.
        /// </param>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ProducerRecord<TKey, TValue>)obj);
        }

        /// <summary>
        ///     Returns a hash code for this <see cref="ProducerRecord{TKey, TValue}"/> value.
        /// </summary>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Topic != null ? Topic.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ EqualityComparer<TKey>.Default.GetHashCode(Key);
                hashCode = (hashCode * 397) ^ EqualityComparer<TValue>.Default.GetHashCode(Value);
                hashCode = (hashCode * 397) ^ Partition;
                hashCode = (hashCode * 397) ^ Timestamp.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        ///     Returns the string representation of the ProducerRecord.
        /// </summary>
        public override string ToString() 
            => $"ProducerRecord({nameof(Topic)}: {Topic}, {nameof(Key)}: {Key}, {nameof(Value)}: {Value}, {nameof(Partition)}: {Partition}, {nameof(Timestamp)}: {Timestamp})";
    }
}
