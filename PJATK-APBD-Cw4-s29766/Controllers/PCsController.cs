using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PJATK_APBD_Cw4_s29766.Data;
using PJATK_APBD_Cw4_s29766.DTOs.GET;
using PJATK_APBD_Cw4_s29766.DTOs.POST;
using PJATK_APBD_Cw4_s29766.DTOs.PUT;
using PJATK_APBD_Cw4_s29766.Models;
using Microsoft.EntityFrameworkCore;

namespace PJATK_APBD_Cw4_s29766.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PCsController : ControllerBase
{
    private readonly AppDbContext _context;

    public PCsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetPCs()
    {
        var pcs = await _context.PCs
            .Select(pc => new GetAllPCsResponse
            {
                Id = pc.Id,
                Name = pc.Name,
                Weight = pc.Weight,
                Warranty = pc.Warranty,
                CreatedAt = pc.CreatedAt,
                Stock = pc.Stock
            })
            .ToListAsync();

        return Ok(pcs);
    }

    [HttpGet("{id}/components")]
    public async Task<IActionResult> GetPCComponents(int id)
    {
        var pc = await _context.PCs
            .Where(p => p.Id == id)
            .Select(p => new GetPCByIdResponse
            {
                Id = p.Id,
                Name = p.Name,
                Weight = p.Weight,
                Warranty = p.Warranty,
                CreatedAt = p.CreatedAt,
                Stock = p.Stock,
                Components = p.PCComponents.Select(pcComponent => new ComponentResponse
                {
                    Code = pcComponent.Component.Code,
                    Name = pcComponent.Component.Name,
                    Amount = pcComponent.Amount
                }).ToList()
            })
            .FirstOrDefaultAsync();

        if (pc == null)
            return NotFound();

        return Ok(pc);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePC(CreatePCRequest request)
    {
        var pc = new PC
        {
            Name = request.Name,
            Weight = request.Weight,
            Warranty = request.Warranty,
            CreatedAt = DateTime.Now,
            Stock = request.Stock
        };

        _context.PCs.Add(pc);
        await _context.SaveChangesAsync();

        return Created($"/api/pcs/{pc.Id}", new
        {
            pc.Id,
            pc.Name,
            pc.Weight,
            pc.Warranty,
            pc.CreatedAt,
            pc.Stock
        });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePC(int id, UpdatePCRequest request)
    {
        var pc = await _context.PCs.FindAsync(id);

        if (pc == null)
            return NotFound();

        pc.Name = request.Name;
        pc.Weight = request.Weight;
        pc.Warranty = request.Warranty;
        pc.Stock = request.Stock;

        await _context.SaveChangesAsync();

        return Ok(new
        {
            pc.Id,
            pc.Name,
            pc.Weight,
            pc.Warranty,
            pc.CreatedAt,
            pc.Stock
        });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePC(int id)
    {
        var pc = await _context.PCs
            .Include(p => p.PCComponents)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (pc == null)
            return NotFound();

        _context.PCComponents.RemoveRange(pc.PCComponents);
        _context.PCs.Remove(pc);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}