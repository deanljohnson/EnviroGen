namespace EnviroGen.Erosion
{
    public class HydraulicErosionData : ErosionData
    {
        /// <summary>
        /// How much rain is deposited in each iteration.
        /// </summary>
        public float RainAmount { get; set; }
        /// <summary>
        /// The amount of land that can be absorbed by water in one iteration.
        /// </summary>
        public float Solubility { get; set; }
        /// <summary>
        /// The percentage of water that evaporates with each iteration.
        /// </summary>
        public float Evaporation { get; set; }
        /// <summary>
        /// The amount of sediment that a unit of water can hold.
        /// </summary>
        public float Capacity { get; set; }
    }
}
