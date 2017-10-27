using KFS.DataStream;

namespace KFS.Disks {
	public enum SectorStatus {
		Unknown,
		Used,
		Free,
		Bad,
		Reserved,
		FAT,
		MasterBootRecord,
		SlackSpace,
		UnknownFilesystem
	};

	/// <summary>
	/// A data source made out of sectors.
	/// </summary>
	public interface IHasSectors : IDataStream {
		ulong GetSectorSize();
		SectorStatus GetSectorStatus(ulong sectorNum);
	}
}
