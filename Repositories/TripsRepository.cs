using System;

public class TripsRepository : ITripsRepository

{
	private readonly ApdbZadanie7Context _context;
	public TripsRepository(ApbdZadanie7Context context)
    {
        _context = context;
    }

    public Task<IEnumerable<TripDTO>> GetTripsAsync()
    {
        var result = _context
            .Trips
            .Select(e => new TripDTO
            {
                Name = e.Name,
                Description = e.Description,
                DateFrom = DateOnly.FromDateTime(e.DateFrom),
                DateTo = DateOnly.FromDateTime(e.DateTo),
                MaxPeople = e.MaxPeople,
                Countries = e.HaCountries,
                CountryDTO = new CountryDTO { Name = e.Name },
                Clients = e.ClientTrips
                    .Select(e => new ClientDTO
                    {
                        FirstName = e.IdClientNavigation.FirstName,
                        LastName = e.IdClientNavigation.LastName
                    })
                    .ToList()
            })
            .ToList();

        return Task.FromResult<IEnumerable<TripDTO>>(result);
    }


}
