﻿using MyImdb.Business.Repositories;
using MyImdb.Entities;

namespace MyImdb.Business.Services;

public class MovieActorService {
	private readonly MovieActorRepository actorRepository;
	private readonly AppDbContext dbContext;

	public MovieActorService(MovieActorRepository actorRepository, AppDbContext dbContext) {
		this.actorRepository = actorRepository;
		this.dbContext = dbContext;
	}

	public async Task<List<Movie>> SelectMoviesByActorIdAsync(Guid id, int n = 20) {
		return await actorRepository.SelectMoviesByActorIdAsync(id, n);
	}

	public async Task<List<Actor>> SelectActorsByMovieIdAsync(Guid id, int n = 20) {
		return await actorRepository.SelectActorsByMovieIdAsync(id, n);
	}
}
