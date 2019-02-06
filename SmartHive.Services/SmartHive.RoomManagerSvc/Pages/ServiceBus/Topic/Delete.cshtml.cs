﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SmartHive.Common.Data;
using SmartHive.RoomManagerSvc.Data;

namespace SmartHive.RoomManagerSvc.Pages.ServiceBus.Topic
{
    public class DeleteModel : PageModel
    {
        private readonly SmartHive.RoomManagerSvc.Data.SmartHiveContext _context;

        public DeleteModel(SmartHive.RoomManagerSvc.Data.SmartHiveContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ServiceBusTopic ServiceBusTopic { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ServiceBusTopic = await _context.ServiceBusTopic
                .Include(s => s.NamespaceNavigation).FirstOrDefaultAsync(m => m.TopicId == id);

            if (ServiceBusTopic == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ServiceBusTopic = await _context.ServiceBusTopic.FindAsync(id);

            if (ServiceBusTopic != null)
            {
                _context.ServiceBusTopic.Remove(ServiceBusTopic);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}