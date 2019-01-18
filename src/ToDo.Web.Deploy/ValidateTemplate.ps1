Param(
    [switch] $ResourceGroupName = "Utopia-Validate",
    [string] $ResourceGroupLocation ="West Europe",
    [string]$TemplateFile = "WebSiteSQLDatabase.json",
    [string]$TemplateParametersFile = "WebSiteSQLDatabase.parameters.json"
)

function Format-ValidationOutput {
    param ($ValidationOutput, [int] $Depth = 0)
    Set-StrictMode -Off
    return @($ValidationOutput | Where-Object { $_ -ne $null } | ForEach-Object { @('  ' * $Depth + ': ' + $_.Message) + @(Format-ValidationOutput @($_.Details) ($Depth + 1)) })
}

New-AzResourceGroup -Name $ResourceGroupName -Location $ResourceGroupLocation -Force

$ErrorMessages = Format-ValidationOutput (Test-AzureRmResourceGroupDeployment -ResourceGroupName $ResourceGroupName `
    -TemplateFile $TemplateFile `
    -TemplateParameterFile $TemplateParametersFile)
if ($ErrorMessages) {
    Write-Output '', 'Validation returned the following errors:', @($ErrorMessages), '', 'Template is invalid.'
}
else {
    Write-Output '', 'Template is valid.'
}