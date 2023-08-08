# deploy.ps1

# Replace with the path to your published output
$sourcePath = "C:\\actions-runner\\_work\\ArmadaMotors.Server\\ArmadaMotors.Server\\ArmadaMotors.Api\\bin\\Release\\net7.0\\win-x64\\publish"

# Replace with the destination path on the server
$destinationPath = "C:\\Muhammadkarim\\Projects\\Armada Motors\\Backend"

# Stop the IIS site before deployment
Write-Host "Stopping the IIS site..."
Stop-WebSite -Name "backend.armadamotor.com"

# Copy all files except the wwwroot folder
Write-Host "Copying files to the destination directory (excluding wwwroot)..."
Get-ChildItem -Path $sourcePath -Exclude "wwwroot" | Copy-Item -Destination $destinationPath -Recurse

# If you have any special requirements for the wwwroot folder, you can copy it separately here
# For example: Copy-Item -Path "$sourcePath\wwwroot\*" -Destination "$destinationPath\wwwroot" -Recurse

# Start the IIS site after deployment
Write-Host "Starting the IIS site..."
Start-WebSite -Name "backend.armadamotor.com"

# Restart the application pool (optional step)
Write-Host "Restarting the application pool..."
Restart-WebAppPool -Name "backend.armadamotor.com"

# If your project requires database migrations or other setup tasks, run them here
# For example, you might run "dotnet ef database update" for Entity Framework migrations

Write-Host "Deployment to IIS completed successfully."
