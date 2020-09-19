﻿using BlazorShared.Models.Appointment;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrontDesk.Blazor.Services
{
    public class AppointmentService
    {
        private readonly HttpService _httpService;
        private readonly ILogger<AppointmentService> _logger;

        public AppointmentService(HttpService httpService, ILogger<AppointmentService> logger)
        {
            _httpService = httpService;
            _logger = logger;
        }

        public async Task<AppointmentDto> CreateAsync(AppointmentDto appointment)
        {
            return (await _httpService.HttpPostAsync<CreateAppointmentResponse>("appointments", appointment)).Appointment;
        }

        public async Task<AppointmentDto> EditAsync(AppointmentDto appointment)
        {
            return (await _httpService.HttpPutAsync<UpdateAppointmentResponse>("appointments", appointment)).Appointment;
        }

        public async Task<string> DeleteAsync(int appointmentId)
        {
            return (await _httpService.HttpDeleteAsync<DeleteAppointmentResponse>("appointments", appointmentId)).Status;
        }

        public async Task<AppointmentDto> GetByIdAsync(int appointmentId)
        {
            return (await _httpService.HttpGetAsync<GetByIdAppointmentResponse>($"appointments/{appointmentId}")).Appointment;
        }

        public async Task<List<AppointmentDto>> ListPagedAsync(int pageSize)
        {
            _logger.LogInformation("Fetching appointments from API.");

            return (await _httpService.HttpGetAsync<ListAppointmentResponse>($"appointments")).Appointments;
        }

        public async Task<List<AppointmentDto>> ListAsync()
        {
            _logger.LogInformation("Fetching appointments from API.");

            return (await _httpService.HttpGetAsync<ListAppointmentResponse>($"appointments")).Appointments;
        }
    }
}
