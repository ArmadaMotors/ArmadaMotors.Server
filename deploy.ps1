 # deploy.ps1\\
$sourcePath = "C:\\actions-runner\\_work\ArmadaMotors.Server\\ArmadaMotors.Server\\ArmadaMotors.Api\\bin\\Release\\net7.0\\win-x64\\publish"  # Replace with the path to your published output
$destinationPath = "C:\\Muhammadkarim\\Projects\\Armada Motors\Backend"  # Replace with the destination path on the server

# Stop the IIS site before deployment
Stop-WebSite -Name "backend.armadamotor.com"

# Remove existing files from the destination directory (optional step)
Remove-Item -Path $destinationPath\* -Recurse -Force

# Copy the published output to the destination directory
Copy-Item -Path $sourcePath\* -Destination $destinationPath -Recurse

# Start the IIS site after deployment
Start-WebSite -Name "backend.armadamotor.com"

# Restart the application pool (optional step)
Restart-WebAppPool -Name "backend.armadamotor.com"

# If your project requires database migrations or other setup tasks, run them here
# For example, you might run "dotnet ef database update" for Entity Framework migrations

Write-Host "Deployment to IIS completed successfully."
 
