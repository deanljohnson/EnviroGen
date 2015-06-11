namespace EnviroGen.Erosion
{
    /// <summary>
    /// Base class for all erosion process data
    /// </summary>
    public abstract class ErosionData
    {
        /// <summary>
        /// The number of times to repeat the erosion process
        /// </summary>
        public int Iterations { get; set; }
    }
}
