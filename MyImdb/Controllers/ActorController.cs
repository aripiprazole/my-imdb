﻿using Api.Actors;
using Microsoft.AspNetCore.Mvc;
using MyImdb.Business.Services;
using MyImdb.Models;

namespace MyImdb.Controllers;

[ApiController]
[Route("api/actors")]
public class ActorController {
	private readonly ActorService actorService;
	private readonly ModelConverter modelConverter;
	private readonly MovieActorService movieActorService;

	public ActorController(
		ActorService actorService,
		MovieActorService movieActorService,
		ModelConverter modelConverter
	) {
		this.actorService = actorService;
		this.movieActorService = movieActorService;
		this.modelConverter = modelConverter;
	}

	[HttpGet("{id:guid}")]
	public async Task<ActorModel> Get(Guid id) {
		var actor = await actorService.SelectByIdAsync(id);

		return modelConverter.ToModel(actor);
	}

	[HttpGet]
	public async Task<List<ActorModel>> List(int n = 20) {
		var actors = await actorService.SelectTopNAsync(n);

		return actors.ConvertAll(modelConverter.ToModel);
	}

	[HttpGet("{id:guid}/movies")]
	public async Task<List<MovieModel>> ListMovies(Guid id, int n = 20) {
		var movies = await movieActorService.SelectMoviesByActorIdAsync(id, n);

		return movies.ConvertAll(modelConverter.ToModel);
	}

	[HttpPost]
	public async Task<ActorModel> Create(ActorData request) {
		var actor = await actorService.CreateAsync(
			request.Name,
			request.Birthplace
		);

		return modelConverter.ToModel(actor);
	}

	[HttpPut("{id:guid}")]
	public async Task<ActorModel> Update(Guid id, ActorData request) {
		var actor = await actorService.SelectByIdAsync(id);

		await actorService.UpdateAsync(
			actor,
			request.Name,
			request.Birthplace
		);

		return modelConverter.ToModel(actor);
	}

	[HttpDelete("{id:guid}")]
	public async Task Delete(Guid id) {
		var actor = await actorService.SelectByIdAsync(id);

		await actorService.DeleteAsync(actor);
	}
}
